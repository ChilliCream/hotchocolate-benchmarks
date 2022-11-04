using HotChocolate.Execution.Benchmarks.Project.Data;
using HotChocolate.Execution.Benchmarks.Project.DataLoader;
using HotChocolate.Execution.Benchmarks.Project.Extensions;

namespace HotChocolate.Execution.Benchmarks.Project.Speakers
{
    [Node]
    [ExtendObjectType(typeof(Speaker))]
    public class SpeakerExtensions
    {
        [UseApplicationDbContext]
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [Parent] Speaker speaker,
            SessionBySpeakerIdDataLoader sessionBySpeakerId,
            CancellationToken cancellationToken)
            => await sessionBySpeakerId.LoadAsync(speaker.Id, cancellationToken);

        [NodeResolver]
        public Task<Speaker> GetSpeakerAsync(
            SpeakerByIdDataLoader speakerById,
            int id,
            CancellationToken cancellationToken) =>
            speakerById.LoadAsync(id, cancellationToken);
    }
}
