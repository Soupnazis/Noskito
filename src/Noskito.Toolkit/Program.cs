using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Noskito.Common.Extension;
using Noskito.Common.Logging;
using Noskito.Database.Extension;
using Noskito.Toolkit.Command;
using Noskito.Toolkit.Parsing;

namespace Noskito.Toolkit
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            
            services.AddLogger();
            services.AddDatabase();
            services.AddImplementingTypes<IParser>();

            var provider = services.BuildServiceProvider();

            var logger = provider.GetRequiredService<ILogger>();
            await Parser.Default.ParseArguments<ParseCommand>(args)
                .WithParsedAsync(async command =>
                {
                    var directory = new DirectoryInfo(command.Path);
                    if (!directory.Exists)
                    {
                        logger.Error($"Can't found directory: {command.Path}");
                        return;
                    }
                    
                    var parsers = provider.GetServices<IParser>();
                    foreach (var parser in parsers)
                    {
                        await parser.Parse(directory);
                    }
                });
        }
    }
}