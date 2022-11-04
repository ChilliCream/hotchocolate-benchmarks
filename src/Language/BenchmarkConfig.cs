using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace HotChocolate.Language.Visitors.Benchmarks;

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        var baseJob = Job.Default;

        AddJob(baseJob
            .WithNuGet("HotChocolate", "12.15.1")
            .WithRuntime(CoreRuntime.Core60));
        AddJob(baseJob
            .WithNuGet("HotChocolate", "12.15.1")
            .WithRuntime(CoreRuntime.Core70));
        AddJob(baseJob
            .WithNuGet("HotChocolate", "13.0.0-preview.76")
            .WithRuntime(CoreRuntime.Core60));
        AddJob(baseJob
            .WithNuGet("HotChocolate", "13.0.0-preview.76")
            .WithRuntime(CoreRuntime.Core70));
    }
}
