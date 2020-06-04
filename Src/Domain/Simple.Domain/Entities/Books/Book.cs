// Copyright (c) simple. All rights reserved.

namespace Simple.Domain.Entities.Books
{
    using System;
    using CSharpFunctionalExtensions;
    using Simple.Infrastructure.Entities;

    public class Book : AggregateRoot<Guid>, ICreationDate, IModificationDate
    {
        private Book()
            : base()
        {
        }

        public string Name { get; private set; }

        public DateTimeOffset Modified { get; private set; }

        public DateTimeOffset Created { get; private set; }

        public static Result<Book> Create(Guid? id, string name)
        {
            if (!id.HasValue || id == Guid.Empty)
            {
                return Result.Failure<Book>("Id is empty");
            }

            var bookToBeCreated = new Book()
            {
                Id = id.Value,
            };

            return Create(bookToBeCreated, name);
        }

        public static Result<Book> Create(string name)
        {
            var bookToBeCreated = new Book()
            {
                Id = Guid.NewGuid(),
            };

            return Create(bookToBeCreated, name);
        }

        public Result<Book> SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<Book>("Name is empty");
            }

            this.Name = name;
            return Result.Ok(this);
        }

        private static Result<Book> Create(Book bookToBeCreated, string name)
        {
            var constraints = Result.Combine(
                bookToBeCreated.SetName(name));

            if (constraints.IsSuccess)
            {
                return Result.Ok(bookToBeCreated);
            }

            return Result.Failure<Book>(constraints.Error);
        }
    }
}