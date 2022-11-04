using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Relay;

namespace HotChocolate.Types.Benchmarks.Project.Sessions
{
    public record AddSessionInput(
        string Title,
        string? Abstract,
        [ID(nameof(Speaker))]
        IReadOnlyList<int> SpeakerIds);
}