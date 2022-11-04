using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Relay;

namespace HotChocolate.Types.Benchmarks.Project.Tracks
{
    public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);
}