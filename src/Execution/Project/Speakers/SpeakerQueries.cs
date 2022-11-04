using HotChocolate.Execution.Benchmarks.Project.Data;
using HotChocolate.Execution.Benchmarks.Project.DataLoader;
using HotChocolate.Execution.Benchmarks.Project.Extensions;

namespace HotChocolate.Execution.Benchmarks.Project.Speakers
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class SpeakerQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Speaker> GetSpeakers(
            [ScopedService] ApplicationDbContext context) =>
            context.Speakers.OrderBy(t => t.Name);

        public Task<Speaker> GetSpeakerByIdAsync(
            [ID(nameof(Speaker))]int id,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))]int[] ids,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);
    }
}