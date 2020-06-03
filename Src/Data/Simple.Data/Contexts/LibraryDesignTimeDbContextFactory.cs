namespace Simple.Data.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using Simple.Infrastructure.Entities.Settings;
    using System;
    using System.IO;

    public class LibraryDesignTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        private const string DATABASE = "Database";

        public LibraryContext CreateDbContext(string[] args)
        {
            // Build config
            var config = new ConfigurationBuilder()
                          .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.Development.json", optional: true)
                          .AddEnvironmentVariables()
                          .Build();

            var database = config.GetSection(DATABASE).Get<Database>();
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();

            optionsBuilder.UseSqlServer(database.LibraryConnectionString);

            return new LibraryContext(optionsBuilder.Options, null);
        }
    }
}
