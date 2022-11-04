using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Types.Benchmarks.Project.Extensions
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UseDbContext<TDbContext>(
            this IObjectFieldDescriptor descriptor)
            where TDbContext : DbContext
        {
            return descriptor.UseScopedService<TDbContext>(
                create: s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
                disposeAsync: (s, c) => c.DisposeAsync());
        }

        public static IObjectFieldDescriptor UseUpperCase(
            this IObjectFieldDescriptor descriptor)
        {
#if HC13
            descriptor.Extend().Definition.FormatterDefinitions.Add(
                new((c, r) => r is string s ? s.ToUpperInvariant() : r));
            return descriptor;
#else 
            descriptor.Extend().Definition.ResultConverters.Add(
                new((c, r) => r is string s ? s.ToUpperInvariant() : r));
            return descriptor;
#endif
        }
    }
}