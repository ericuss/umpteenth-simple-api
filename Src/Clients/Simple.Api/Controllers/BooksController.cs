// Copyright (c) simple. All rights reserved.

namespace Simple.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Domain.Entities.Books;
    using Simple.Infrastructure.ControllersCore;
    using Simple.Infrastructure.Repository;

    public class BooksController : ControllerCore
    {
        private readonly IRepositoryReadOnly<Book, Guid> _repository;

        public BooksController(IRepositoryReadOnly<Book, Guid> repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var books = await this._repository.GetAsync();
            return this.Ok(books.ToList());
        }
    }
}
