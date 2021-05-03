using System;
using Microsoft.Extensions.DependencyInjection;
using Noskito.Database.Abstraction.Repository;
using Noskito.Database.Repository;

namespace Noskito.Database.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddSingleton<DbContextFactory>();
        }
    }
}