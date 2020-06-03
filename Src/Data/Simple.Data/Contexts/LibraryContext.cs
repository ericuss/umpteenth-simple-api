// Copyright (c) simple. All rights reserved.

namespace Simple.Data.Contexts
{
    using Simple.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using Simple.Domain.Entities.Books;

    public class LibraryContext : ContextCore
    {
        public LibraryContext(DbContextOptions<LibraryContext> options, IHostEnvironment env)
            : base(options, env)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
    }
}
