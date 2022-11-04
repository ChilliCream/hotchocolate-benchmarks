using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Benchmarks.Project.DataLoader;
using HotChocolate.Types.Relay;

namespace HotChocolate.Types.Benchmarks.Project.Attendees
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class AttendeeSubscriptions
    {
        [Subscribe(With = nameof(SubscribeToOnAttendeeCheckedInAsync))]
        public SessionAttendeeCheckIn OnAttendeeCheckedIn(
            [ID(nameof(Session))] int sessionId,
            [EventMessage] int attendeeId,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            new SessionAttendeeCheckIn(attendeeId, sessionId);

        public async ValueTask<ISourceStream<int>> SubscribeToOnAttendeeCheckedInAsync(
            int sessionId,
            [Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken) =>
            await eventReceiver.SubscribeAsync<string, int>(
                "OnAttendeeCheckedIn_" + sessionId, cancellationToken);
    }
}