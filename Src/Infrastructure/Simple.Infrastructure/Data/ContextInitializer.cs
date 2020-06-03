// Copyright (c) Simple. All rights reserved.

namespace Simple.Data.Contexts.Core
{
    using System;
    using System.Threading.Tasks;
    using Simple.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class ContextInitializer<TContext>
        where TContext : ContextCore
    {
        public async Task ConfigureDB(IServiceCollection services, string connectionString, bool migrate, IHostEnvironment env)
        {
            this.AddToDI(services, connectionString);
            await this.Initialize(connectionString, migrate, env);
        }

        private void AddToDI(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TContext>(o => o.UseSqlServer(connectionString));
        }

        private Task Initialize(string connectionString, bool migrate, IHostEnvironment env)
        {
            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(connectionString,
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });

            var context = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { optionsBuilder.Options, env });
            if (migrate)
            {
                //if (settings.Database.EnsureDeleted)
                //{
                //    await context.Database.EnsureDeletedAsync();
                //}

                // if (!await context.Database.EnsureCreatedAsync())
                // {
                context.Database.Migrate();

                // }
            }

            // temp
            return Task.CompletedTask;
        }
    }
}