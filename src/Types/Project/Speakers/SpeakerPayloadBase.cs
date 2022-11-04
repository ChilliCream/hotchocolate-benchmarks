using HotChocolate.Types.Benchmarks.Project.Common;
using HotChocolate.Types.Benchmarks.Project.Data;

namespace HotChocolate.Types.Benchmarks.Project.Speakers
{
    public class SpeakerPayloadBase : Payload
    {
        protected SpeakerPayloadBase(Speaker speaker)
        {
            Speaker = speaker;
        }

        protected SpeakerPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Speaker? Speaker { get; }
    }
}