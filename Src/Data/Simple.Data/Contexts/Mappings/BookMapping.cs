// Copyright (c) simple. All rights reserved.

namespace Simple.Data.Contexts.Mappings
{
    using Simple.Domain.Entities.Books;
    using Simple.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookMapping : EntityMappingConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name)
                  .IsRequired(true);
        }
    }
}
