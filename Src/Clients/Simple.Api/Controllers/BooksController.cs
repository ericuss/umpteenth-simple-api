// Copyright (c) simple. All rights reserved.

namespace Simple.Api.Controllers
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using Simple.Application.Commands.BookCommand;
    using Simple.Application.Queries.GetBooks;
    using Simple.Infrastructure.ControllersCore;

    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class BooksController : ControllerCore
    {
        private readonly IMediator _mediator;
        private readonly IHostEnvironment _env;

        public BooksController(IMediator mediator, IHostEnvironment env)
        {
            this._mediator = mediator;
            this._env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await this._mediator.Send(new GetBooksQuery());

            if (books.IsFailure)
            {
                return this.BadRequest(this._env.IsDevelopment() ? books.Error : string.Empty);
            }

            return this.Ok(books.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // apply fluent validation
            var books = await this._mediator.Send(new CreateBookCommand());

            if (books.IsFailure)
            {
                return this.BadRequest(this._env.IsDevelopment() ? books.Error : string.Empty);
            }

            return this.Ok(books.Value);
        }
    }
}
