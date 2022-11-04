using HotChocolate.Execution.Benchmarks.Project.Data;

namespace HotChocolate.Execution.Benchmarks.Project.Sessions
{
    public record RenameSessionInput(
        [ID(nameof(Session))] int SessionId,
        string Title);
}