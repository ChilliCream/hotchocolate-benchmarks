using HotChocolate.Execution.Benchmarks.Project.Data;

namespace HotChocolate.Execution.Benchmarks.Project.Tracks
{
    public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);
}