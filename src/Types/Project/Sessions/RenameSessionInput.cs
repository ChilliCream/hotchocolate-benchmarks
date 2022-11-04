using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Relay;

namespace HotChocolate.Types.Benchmarks.Project.Sessions
{
    public record RenameSessionInput(
        [ID(nameof(Session))] int SessionId,
        string Title);
}