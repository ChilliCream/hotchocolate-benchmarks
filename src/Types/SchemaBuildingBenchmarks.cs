using BenchmarkDotNet.Attributes;
using HotChocolate.Execution;
using HotChocolate.Types.Benchmarks.Project.Attendees;
using HotChocolate.Types.Benchmarks.Project.DataLoader;
using HotChocolate.Types.Benchmarks.Project.Sessions;
using HotChocolate.Types.Benchmarks.Project.Speakers;
using HotChocolate.Types.Benchmarks.Project.Tracks;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Benchmarks;

[Config(typeof(BenchmarkConfig))]
[RankColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
public class SchemaBuildingBenchmarks
{
    private readonly ServiceCollection _services = new();

    public SchemaBuildingBenchmarks()
    {
        _services

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

                    // Since we are using subscriptions, we need to register a pub/sub system.
                    // for our demo we are using a in-memory pub/sub system.
                    .AddInMemorySubscriptions();
    }


    [Benchmark]
    public async Task<IRequestExecutor> Build_Small_Schema()
    {
        await using var services = _services.BuildServiceProvider();
        return await services.GetRequiredService<IRequestExecutorResolver>().GetRequestExecutorAsync();
    }
}