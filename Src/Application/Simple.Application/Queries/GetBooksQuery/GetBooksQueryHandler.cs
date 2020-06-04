// Copyright (c) simple. All rights reserved.

namespace Simple.Application.Queries.GetBooks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Simple.Domain.Entities.Books;
    using Simple.Infrastructure.Repository;

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Result<List<Book>>>
    {
        private readonly IRepositoryReadOnly<Book, Guid> repository;

        public GetBooksQueryHandler(IRepositoryReadOnly<Book, Guid> repository)
        {
            this.repository = repository;
        }

        public async Task<Result<List<Book>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await this.repository.GetAsync();
            return Result.Ok(books.ToList());
        }
    }
}
