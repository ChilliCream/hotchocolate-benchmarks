using HotChocolate.Execution.Benchmarks.Project.Data;

namespace HotChocolate.Execution.Benchmarks.Project.Sessions
{
    public record AddSessionInput(
        string Title,
        string? Abstract,
        [ID(nameof(Speaker))]
        IReadOnlyList<int> SpeakerIds);
}