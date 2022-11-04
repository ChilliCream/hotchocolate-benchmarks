using HotChocolate.Types.Benchmarks.Project.Common;
using HotChocolate.Types.Benchmarks.Project.Data;

namespace HotChocolate.Types.Benchmarks.Project.Tracks
{
    public class RenameTrackPayload : TrackPayloadBase
    {
        public RenameTrackPayload(Track track) 
            : base(track)
        {
        }

        public RenameTrackPayload(IReadOnlyList<UserError> errors) 
            : base(errors)
        {
        }
    }
}