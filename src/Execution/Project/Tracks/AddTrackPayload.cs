using HotChocolate.Execution.Benchmarks.Project.Common;
using HotChocolate.Execution.Benchmarks.Project.Data;

namespace HotChocolate.Execution.Benchmarks.Project.Tracks
{
    public class AddTrackPayload : TrackPayloadBase
    {
        public AddTrackPayload(Track track) 
            : base(track)
        {
        }

        public AddTrackPayload(IReadOnlyList<UserError> errors) 
            : base(errors)
        {
        }
    }
}