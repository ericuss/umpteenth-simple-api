// Copyright (c) simple. All rights reserved.

namespace Simple.Data
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Simple.Data.Contexts;
    using Simple.Data.Repositories;
    using Simple.Data.Repositories.Core;
    using Simple.Domain.Entities.Books;
    using Simple.Infrastructure.Repository;

    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, string connectionString, bool migrate, IHostEnvironment env)
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .RegisterRepositories()
                .RegisterReadOnlyRepositories()
                .RegisterDB(connectionString, migrate, env)
                ;

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Book, Guid>, BookRepository>();

            return services;
        }

        private static IServiceCollection RegisterReadOnlyRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryReadOnly<Book, Guid>, BookRepository>();

            return services;
        }

        private static IServiceCollection RegisterDB(this IServiceCollection services, string connectionString, bool migrate, IHostEnvironment env)
        {
            LibraryContextInitializer.Instance.ConfigureDB(
                services,
                connectionString,
                migrate,
                env).Wait();
            return services;
        }
    }
}
