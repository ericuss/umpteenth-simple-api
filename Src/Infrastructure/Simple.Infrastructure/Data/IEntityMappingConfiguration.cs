// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public interface IEntityMappingConfiguration
    {
        void Configure(ModelBuilder b);
    }

    public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration
        where T : class
    {
        void Configure(EntityTypeBuilder<T> builder);
    }
}