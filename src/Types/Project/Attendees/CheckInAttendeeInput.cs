using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Relay;

namespace HotChocolate.Types.Benchmarks.Project.Attendees
{
    public record CheckInAttendeeInput(
        [ID(nameof(Session))]
        int SessionId,
        [ID(nameof(Attendee))]
        int AttendeeId);
}