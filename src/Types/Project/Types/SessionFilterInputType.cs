using HotChocolate.Data.Filters;
using HotChocolate.Types.Benchmarks.Project.Data;

namespace HotChocolate.Types.Benchmarks.Project.Types
{
    public class SessionFilterInputType : FilterInputType<Session>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Session> descriptor)
        {
            descriptor.Ignore(t => t.Id);
            descriptor.Ignore(t => t.TrackId);
        }
    }
}