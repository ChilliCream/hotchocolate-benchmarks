using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Relay;

namespace HotChocolate.Types.Benchmarks.Project.Sessions
{
    public record ScheduleSessionInput(
        [ID(nameof(Session))]
        int SessionId,
        [ID(nameof(Track))]
        int TrackId,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime);
}