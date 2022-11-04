using HotChocolate.Execution.Benchmarks.Project.Common;
using HotChocolate.Execution.Benchmarks.Project.Data;

namespace HotChocolate.Execution.Benchmarks.Project.Sessions
{
    public class RenameSessionPayload : Payload
    {
        public RenameSessionPayload(Session session)
        {
            Session = session;
        }

        public RenameSessionPayload(UserError error)
            : base(new[] { error })
        {
        }

        public Session? Session { get; }
    }
}