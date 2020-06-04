// Copyright (c) simple. All rights reserved.

namespace Simple.Application.Commands.BookCommand
{
    using System;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Simple.Domain.Entities.Books;

    public class DeleteBookCommand : IRequest<Result<Book>>
    {
        public DeleteBookCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
