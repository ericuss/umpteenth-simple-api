// Copyright (c) simple. All rights reserved.

namespace Simple.Application.Commands.BookCommand
{
    using System;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Simple.Domain.Entities.Books;

    public class UpdateBookCommand : IRequest<Result<Book>>
    {
        public UpdateBookCommand(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
