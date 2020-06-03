// Copyright (c) simple. All rights reserved.

namespace Simple.Data.Repositories
{
    using System;
    using Simple.Data.Contexts;
    using Simple.Data.Repositories.Core;
    using Simple.Domain.Entities.Books;

    public class BookRepository : Repository<Book, Guid>
    {
        public BookRepository(LibraryContext context)
            : base(context)
        {
        }
    }
}
