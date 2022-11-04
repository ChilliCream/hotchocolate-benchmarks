using HotChocolate.Execution.Benchmarks.Project.Data;
using HotChocolate.Execution.Benchmarks.Project.DataLoader;

namespace HotChocolate.Execution.Benchmarks.Project.Sessions
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class SessionSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Session> OnSessionScheduledAsync(
            [EventMessage] int sessionId,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(sessionId, cancellationToken);
    }
}