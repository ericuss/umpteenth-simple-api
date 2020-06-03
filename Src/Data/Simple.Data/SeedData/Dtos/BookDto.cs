// Copyright (c) Simple. All rights reserved.

namespace Simple.Data.SeedData.Mappers
{
    using System;
    using CSharpFunctionalExtensions;
    using Simple.Domain.Entities.Books;

    public class BookDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Result<Book> MapToEntity()
        {
            var book = Book.Create(this.Id, this.Name);

            return book;
        }
    }
}
