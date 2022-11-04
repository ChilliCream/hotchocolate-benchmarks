using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace HotChocolate.Types.Benchmarks;

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        var baseJob = Job.Default;

        AddJob(baseJob
            .WithCustomBuildConfiguration("HC_12")
            .WithRuntime(CoreRuntime.Core60));
        AddJob(baseJob
            .WithCustomBuildConfiguration("HC_12")
            .WithRuntime(CoreRuntime.Core70));
        AddJob(baseJob
            .WithCustomBuildConfiguration("HC_13")
            .WithRuntime(CoreRuntime.Core60));
        AddJob(baseJob
            .WithCustomBuildConfiguration("HC_13")
            .WithRuntime(CoreRuntime.Core70));
    }
}
