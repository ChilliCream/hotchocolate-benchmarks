using BenchmarkDotNet.Attributes;
using HotChocolate.Execution.Benchmarks.Project;
using HotChocolate.Execution.Benchmarks.Requests;

namespace HotChocolate.Execution.Benchmarks;

[Config(typeof(BenchmarkConfig))]
[RPlotExporter, CategoriesColumn, RankColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
public class IntrospectionBenchmarks : BenchmarkBase
{
    [Benchmark]
    public async Task Query_TypeName() =>
        await BenchmarkAsync(@"
                {
                    __typename
                }
            ");

    [Benchmark]
    public async Task Query_Introspection() =>
        await BenchmarkAsync(Introspection);
    public string Introspection { get; } = Resources.Introspection;
        
    [Benchmark]
    public async Task Query_Introspection_Prepared() =>
        await BenchmarkAsync(IntrospectionRequest);
    public IQueryRequest IntrospectionRequest { get; } = Prepare(Resources.Introspection);
}