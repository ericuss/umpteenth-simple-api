// Copyright (c) simple. All rights reserved.

namespace Simple.Application.Commands.BookCommand
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Simple.Domain.Entities.Books;

    public class CreateBookCommand : IRequest<Result<Book>>
    {
        public CreateBookCommand(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
