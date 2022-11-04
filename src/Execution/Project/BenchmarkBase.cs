using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using HotChocolate.Execution.Benchmarks.Project.Attendees;
using HotChocolate.Execution.Benchmarks.Project.Data;
using HotChocolate.Execution.Benchmarks.Project.DataLoader;
using HotChocolate.Execution.Benchmarks.Project.Imports;
using HotChocolate.Execution.Benchmarks.Project.Sessions;
using HotChocolate.Execution.Benchmarks.Project.Speakers;
using HotChocolate.Execution.Benchmarks.Project.Tracks;
using HotChocolate.Language;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Execution.Benchmarks.Project
{
    public class BenchmarkBase
    {
        private static readonly MD5DocumentHashProvider _md5 = new();
        private static readonly JsonSerializerOptions _options =
            new(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        private readonly Uri _url;

        public BenchmarkBase()
        {
            var servicesCollection = new ServiceCollection();
            ConfigureServices(servicesCollection);
            Services = servicesCollection.BuildServiceProvider();
            ExecutorResolver = Services.GetRequiredService<IRequestExecutorResolver>();

            TestServerFactory.CreateServer(
                services =>
                {
                    services.AddGraphQLServer();
                    ConfigureServices(services);
                },
                out var port);

            _url = new Uri($"http://localhost:{port}/graphql");
            TestClient = new HttpClient();
        }

        public IServiceProvider Services { get; }

        public IRequestExecutorResolver ExecutorResolver { get; }

        public HttpClient TestClient { get; }

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            foreach (var method in GetType().GetMethods()
                .Where(t => t.IsDefined(typeof(BenchmarkAttribute))))
            {
                Console.WriteLine("Initialize: " + method.Name);
                if (method.Invoke(this, Array.Empty<object>()) is Task task)
                {
                    await task.ConfigureAwait(false);
                }
            }
        }

        public Task BenchmarkServerAsync(string requestDocument)
            => BenchmarkServerAsync(new ClientQueryRequest { Query = requestDocument });

        public Task BenchmarkServerAsync(ClientQueryRequest request)
            => PostAsync(request);

        public Task BenchmarkAsync(string requestDocument)
        {
            return BenchmarkAsync(new QueryRequest(new QuerySourceText(requestDocument)));
        }

        public async Task BenchmarkAsync(IQueryRequest request)
        {
            var executor = await ExecutorResolver.GetRequestExecutorAsync();
            var result = await executor.ExecuteAsync(request);

            if (result is IQueryResult cr && cr.Errors is { Count: > 0 })
            {
                throw new InvalidOperationException("The request failed.");
            }

            if (result is IAsyncDisposable d)
            {
                await d.DisposeAsync();
            }
        }

        protected static IQueryRequest Prepare(string requestDocument)
        {
            var hash = _md5.ComputeHash(Encoding.UTF8.GetBytes(requestDocument).AsSpan());
            var document = Utf8GraphQLParser.Parse(requestDocument);

            return QueryRequestBuilder.New()
                .SetQuery(document)
                .SetQueryHash(hash)
                .SetQueryId(hash)
                .Create();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                // First we add the DBContext which we will be using to interact with our
                // Database.
                .AddPooledDbContextFactory<ApplicationDbContext>(
                    options => options.UseSqlite("Data Source=conferences.db"))

                // This adds the GraphQL server core service and declares a schema.
                .AddGraphQL()

                    // Next we add the types to our schema.
                    .AddQueryType()
                        .AddTypeExtension<AttendeeQueries>()
                        .AddTypeExtension<SessionQueries>()
                        .AddTypeExtension<SpeakerQueries>()
                        .AddTypeExtension<TrackQueries>()
                    .AddMutationType()
                        .AddTypeExtension<AttendeeMutations>()
                        .AddTypeExtension<SessionMutations>()
                        .AddTypeExtension<SpeakerMutations>()
                        .AddTypeExtension<TrackMutations>()
                    .AddSubscriptionType()
                        .AddTypeExtension<AttendeeSubscriptions>()
                        .AddTypeExtension<SessionSubscriptions>()
                    .AddTypeExtension<AttendeeExtensions>()
                    .AddTypeExtension<SessionExtensions>()
                    .AddTypeExtension<TrackExtensions>()
                    .AddTypeExtension<SpeakerExtensions>()

                    // In this section we are adding extensions like relay helpers,
                    // filtering and sorting.
                    .AddFiltering()
                    .AddSorting()
                    .AddGlobalObjectIdentification()
                    .AddQueryFieldToMutationPayloads()
                    .AddIdSerializer()

                    // Now we add some the DataLoader to our system.
                    .AddDataLoader<AttendeeByIdDataLoader>()
                    .AddDataLoader<SessionByIdDataLoader>()
                    .AddDataLoader<SessionBySpeakerIdDataLoader>()
                    .AddDataLoader<SpeakerByIdDataLoader>()
                    .AddDataLoader<SpeakerBySessionIdDataLoader>()
                    .AddDataLoader<TrackByIdDataLoader>()

                    // .AddDiagnosticEventListener<BatchDataLoaderDiagnostics>()
                    // .AddDiagnosticEventListener<BatchExecutionDiagnostics>()

                    // we make sure that the db exists and prefill it with conference data.
                    .EnsureDatabaseIsCreated()

                    // Since we are using subscriptions, we need to register a pub/sub system.
                    // for our demo we are using a in-memory pub/sub system.
                    .AddInMemorySubscriptions();
        }

        private async Task PostAsync(ClientQueryRequest request)
        {
            using var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(request, _options),
                Encoding.UTF8,
                "application/json");

            using var requestMsg = new HttpRequestMessage(HttpMethod.Post, _url)
            {
                Content = content
            };

            using var responseMsg = await TestClient.SendAsync(requestMsg);

            if (responseMsg.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed.");
            }
        }
    }

    public class ClientQueryRequest
    {
        public string? Id { get; set; }

        public string? OperationName { get; set; }

        public string? Query { get; set; }

        public Dictionary<string, object>? Variables { get; set; }

        public Dictionary<string, object>? Extensions { get; set; }
    }

    public class ClientQueryResponse
    {
        public string ContentType { get; set; } = default!;

        public HttpStatusCode StatusCode { get; set; }

        public Dictionary<string, object>? Data { get; set; }

        public List<Dictionary<string, object>>? Errors { get; set; }

        public Dictionary<string, object>? Extensions { get; set; }
    }

    public static class TestServerFactory
    {
        public static IWebHost CreateServer(Action<IServiceCollection> configure, out int port)
        {
            for (port = 5500; port < 6000; port++)
            {
                try
                {
                    var configBuilder = new ConfigurationBuilder();
                    configBuilder.AddInMemoryCollection();
                    var config = configBuilder.Build();
                    config["server.urls"] = $"http://localhost:{port}";
                    var host = new WebHostBuilder()
                        .UseConfiguration(config)
                        .UseKestrel()
                        .ConfigureServices(services =>
                        {
                            services.AddHttpContextAccessor();
                            services.AddRouting();
                            configure(services);
                        })
                        .Configure(app =>
                        {
                            app.UseRouting();
                            app.UseEndpoints(c => c.MapGraphQL());
                        })
                        .Build();

                    host.Start();

                    return host;
                }
                catch
                {
                    // ignored
                }
            }

            throw new InvalidOperationException("Not port found");
        }
    }
}
