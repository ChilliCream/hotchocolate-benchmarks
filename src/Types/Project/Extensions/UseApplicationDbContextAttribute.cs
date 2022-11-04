using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Types.Benchmarks.Project.Data;
using HotChocolate.Types.Descriptors;

namespace HotChocolate.Types.Benchmarks.Project.Extensions
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public UseApplicationDbContextAttribute([CallerLineNumber] int order = 0)
        {
            Order = order;
        }

        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<ApplicationDbContext>();
        }
    }
}