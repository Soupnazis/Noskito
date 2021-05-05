using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Noskito.Common.Logging;

namespace Noskito.Common.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, SerilogLogger>();
        }

        public static void AddImplementingTypes<T>(this IServiceCollection services)
        {
            var types = typeof(T).Assembly.GetTypes()
                .Where(x => typeof(T).IsAssignableFrom(x))
                .Where(x => !x.IsAbstract && !x.IsInterface);

            foreach (var type in types) services.AddTransient(typeof(T), type);
        }
    }
}