﻿using Microsoft.Extensions.DependencyInjection;
using Noskito.Database.Repository;

namespace Noskito.Database.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddTransient<AccountRepository>();
            services.AddTransient<CharacterRepository>();
            services.AddSingleton<DbContextFactory>();
        }
    }
}