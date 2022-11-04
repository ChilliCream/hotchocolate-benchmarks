using HotChocolate.Execution.Benchmarks.Project.Data;

namespace HotChocolate.Execution.Benchmarks.Project.Attendees
{
    public record CheckInAttendeeInput(
        [ID(nameof(Session))]
        int SessionId,
        [ID(nameof(Attendee))]
        int AttendeeId);
}