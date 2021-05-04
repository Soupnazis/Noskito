﻿using Microsoft.Extensions.DependencyInjection;
using Noskito.Common.Extension;


namespace Noskito.World.Processor.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPacketProcessing(this IServiceCollection services)
        {
            services.AddSingleton<ProcessorManager>();
            services.AddImplementingTypes<IPacketProcessor>();
        }
    }
}