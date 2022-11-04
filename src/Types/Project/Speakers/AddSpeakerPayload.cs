using HotChocolate.Types.Benchmarks.Project.Common;
using HotChocolate.Types.Benchmarks.Project.Data;

namespace HotChocolate.Types.Benchmarks.Project.Speakers
{
    public class AddSpeakerPayload : SpeakerPayloadBase
    {
        public AddSpeakerPayload(Speaker speaker)
            : base(speaker)
        {
        }

        public AddSpeakerPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}