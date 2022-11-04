using System.Reflection;
using HotChocolate.Types.Descriptors;

namespace HotChocolate.Execution.Benchmarks.Project.Extensions
{
    public class UseUpperCaseAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context, 
            IObjectFieldDescriptor descriptor, 
            MemberInfo member)
        {
            descriptor.UseUpperCase();
        }
    }
}