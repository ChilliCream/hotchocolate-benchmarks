using HotChocolate.Types.Benchmarks.Project.Common;
using HotChocolate.Types.Benchmarks.Project.Data;

namespace HotChocolate.Types.Benchmarks.Project.Tracks
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