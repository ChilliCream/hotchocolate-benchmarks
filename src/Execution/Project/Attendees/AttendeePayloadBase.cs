using HotChocolate.Execution.Benchmarks.Project.Common;
using HotChocolate.Execution.Benchmarks.Project.Data;

namespace HotChocolate.Execution.Benchmarks.Project.Attendees
{
    public class AttendeePayloadBase : Payload
    {
        protected AttendeePayloadBase(Attendee attendee)
        {
        }

        protected AttendeePayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Attendee? Attendee { get; }
    }
}