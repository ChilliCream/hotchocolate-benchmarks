using HotChocolate.Execution.Benchmarks.Project.Data;
using HotChocolate.Execution.Benchmarks.Project.DataLoader;
using HotChocolate.Execution.Benchmarks.Project.Extensions;

namespace HotChocolate.Execution.Benchmarks.Project.Attendees
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AttendeeQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Attendee> GetAttendees(
            [ScopedService] ApplicationDbContext context) => 
            context.Attendees;

        public Task<Attendee> GetAttendeeByIdAsync(
            [ID(nameof(Attendee))]int id,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken) => 
            attendeeById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Attendee>> GetAttendeesByIdAsync(
            [ID(nameof(Attendee))]int[] ids,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken) => 
            await attendeeById.LoadAsync(ids, cancellationToken);
    }
}