using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Benchmarks.Project.DataLoader;
using HotChocolate.Types.Benchmarks.Project.Extensions;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace HotChocolate.Types.Benchmarks.Project.Tracks
{
    [Node]
    [ExtendObjectType(typeof(Track))]
    public class TrackExtensions
    {
        [UseUpperCase]
        public string? GetName([Parent] Track track) => track.Name;
        
        [UseApplicationDbContext]
        [UsePaging]
        [BindMember(nameof(Track.Sessions))]
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [Parent] Track track,
            [ScopedService] ApplicationDbContext dbContext,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken)
        {
            var sessionIds = await dbContext.Sessions
                .Where(s => s.Id == track.Id)
                .Select(s => s.Id)
                .ToArrayAsync(cancellationToken);

            return await sessionById.LoadAsync(sessionIds, cancellationToken);
        }
        
        [NodeResolver]
        public Task<Track> GetTrackAsync(
            TrackByIdDataLoader trackById,
            int id,
            CancellationToken cancellationToken) =>
            trackById.LoadAsync(id, cancellationToken);
    }
}