using BenchmarkDotNet.Attributes;
using HotChocolate.Execution.Benchmarks.Project;
using HotChocolate.Execution.Benchmarks.Requests;

namespace HotChocolate.Execution.Benchmarks;

[Config(typeof(BenchmarkConfig))]
[RPlotExporter, CategoriesColumn, RankColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
public class QueryBenchmarks : BenchmarkBase
{
    [Benchmark]
    public async Task Sessions_TitleAndAbstract_10_Items() =>
        await BenchmarkAsync(@"
                {
                    sessions(first: 10) {
                        nodes {
                            title
                            abstract
                        }
                    }
                }
            ");

    [Benchmark]
    public async Task Sessions_TitleAndAbstractAndTrackName_10_Items() =>
        await BenchmarkAsync(@"
                {
                    sessions(first: 10) {
                        nodes {
                            title
                            abstract
                            track {
                                name
                            }
                        }
                    }
                }
            ");

    [Benchmark]
    public async Task Sessions_Medium() =>
        await BenchmarkAsync(SessionMediumQuery);

    public string SessionMediumQuery { get; } = Resources.SessionMediumQuery;

    [Benchmark]
    public async Task Sessions_Large() =>
        await BenchmarkAsync(SessionLargeQuery);

    public string SessionLargeQuery { get; } = Resources.SessionLargeQuery;

    [Benchmark]
    public async Task Sessions_DataLoader_Large() =>
        await BenchmarkAsync(SessionLargeDataLoaderQuery);

    public string SessionLargeDataLoaderQuery { get; } = Resources.SessionLargeDLQuery;
}