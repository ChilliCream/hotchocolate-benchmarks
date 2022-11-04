using System.Text;
using BenchmarkDotNet.Attributes;
using HotChocolate.Language.Benchmarks.Resources;

namespace HotChocolate.Language.Benchmarks;

[Config(typeof(BenchmarkConfig))]
[RankColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
public class ParserBenchmarks
{
    private readonly byte[] _introspectionBytes;
    private readonly string _introspectionString;
    private readonly byte[] _kitchenSinkSchemaBytes;
    private readonly string _kitchenSinkSchemaString;
    private readonly byte[] _kitchenSinkBytes;
    private readonly string _kitchenSinkString;
    private readonly byte[] _largeSchemaBytes;
    private readonly string _largeSchemaString;
    private readonly byte[] _mediumSchemaBytes;
    private readonly string _mediumSinkString;

    public ParserBenchmarks()
    {
        var resources = new ResourceHelper();
        _introspectionString = resources.GetResourceString("IntrospectionQuery.graphql");
        _introspectionBytes = Encoding.UTF8.GetBytes(_introspectionString);
        _kitchenSinkSchemaString = resources.GetResourceString("schema-kitchen-sink.graphql");
        _kitchenSinkSchemaBytes = Encoding.UTF8.GetBytes(_kitchenSinkSchemaString);
        _kitchenSinkString = resources.GetResourceString("kitchen-sink-nullability.graphql");
        _kitchenSinkBytes = Encoding.UTF8.GetBytes(_kitchenSinkString);
        _largeSchemaString = resources.GetResourceString("kitchen-sink-nullability.graphql");
        _largeSchemaBytes = Encoding.UTF8.GetBytes(_largeSchemaString);
        _mediumSinkString = resources.GetResourceString("kitchen-sink-nullability.graphql");
        _mediumSchemaBytes = Encoding.UTF8.GetBytes(_mediumSinkString);
    }

    [Benchmark]
    public DocumentNode Introspection_Parse()
        => Utf8GraphQLParser.Parse(_introspectionBytes);

    [Benchmark]
    public DocumentNode KitchenSink_Query_Parse()
    => Utf8GraphQLParser.Parse(_kitchenSinkBytes);

    [Benchmark]
    public DocumentNode KitchenSink_Schema_Parse()
        => Utf8GraphQLParser.Parse(_kitchenSinkSchemaBytes);

    [Benchmark]
    public DocumentNode Medium_Schema()
        => Utf8GraphQLParser.Parse(_mediumSchemaBytes);

    [Benchmark]
    public DocumentNode Large_Schema()
        => Utf8GraphQLParser.Parse(_largeSchemaBytes);
}
