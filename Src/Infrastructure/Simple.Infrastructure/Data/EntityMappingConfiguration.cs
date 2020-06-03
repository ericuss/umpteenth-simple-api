// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration<T>
        where T : class
    {
        public abstract void Configure(EntityTypeBuilder<T> entity);

        public void Configure(ModelBuilder entityBuilder)
        {
            var entity = entityBuilder.Entity<T>();
            this.Configure(entity);
        }
    }
}