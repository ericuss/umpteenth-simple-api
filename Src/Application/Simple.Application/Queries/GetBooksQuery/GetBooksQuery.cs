// Copyright (c) simple. All rights reserved.

namespace Simple.Application.Queries.GetBooks
{
    using System.Collections.Generic;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Simple.Domain.Entities.Books;

    public class GetBooksQuery : IRequest<Result<List<Book>>>
    {
    }
}
