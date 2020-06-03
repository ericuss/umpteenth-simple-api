// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Simple.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;

    public abstract class ContextCore : DbContext
    {
        private readonly IHostEnvironment _env;

        protected ContextCore(DbContextOptions options, IHostEnvironment env)
            : base(options)
        {
            this._env = env;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            this.UpdateTimestampOfEntities();
            return base.SaveChanges();
        }

        protected void UpdateTimestampOfEntities()
        {
            const string ModificationProperty = "Modification";

            var entries = this.ChangeTracker.Entries().Where(p =>
                                                            p.State == EntityState.Modified
                                                            && p.Entity.GetType().Assembly.DefinedTypes.Any(x => typeof(IModificationDate).IsAssignableFrom(x)));
            foreach (var entry in entries)
            {
                if (entry.Metadata.FindProperty(ModificationProperty) != null)
                {
                    entry.Property(ModificationProperty).CurrentValue = DateTimeOffset.UtcNow;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var mappings = this.GetType().Assembly.DefinedTypes.Where(t =>
                typeof(IEntityMappingConfiguration).IsAssignableFrom(t));

            foreach (var type in mappings.Where(m => !m.IsAbstract && !m.IsInterface))
            {
                var builder = (IEntityMappingConfiguration)Activator.CreateInstance(type);
                builder.Configure(modelBuilder);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this._env.IsDevelopment())
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
