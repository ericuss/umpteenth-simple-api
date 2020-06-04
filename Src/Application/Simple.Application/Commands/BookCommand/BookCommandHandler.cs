// Copyright (c) simple. All rights reserved.

namespace Simple.Application.Commands.BookCommand
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Simple.Domain.Entities.Books;
    using Simple.Infrastructure.Repository;

    public class BookCommandHandler : IRequestHandler<CreateBookCommand, Result<Book>>,
                                      IRequestHandler<UpdateBookCommand, Result<Book>>,
                                      IRequestHandler<DeleteBookCommand, Result<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Book, Guid> _repository;

        public BookCommandHandler(IUnitOfWork unitOfWork, IRepository<Book, Guid> repository)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
        }

        public async Task<Result<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var bookResult = Book.Create(request.Name);
            if (bookResult.IsFailure)
            {
                return Result.Failure<Book>(bookResult.Error);
            }

            this._repository.Add(bookResult.Value);
            await this._unitOfWork.SaveChangesAsync();

            return Result.Ok(bookResult.Value);
        }

        public async Task<Result<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await this._repository.GetByIdAsync(request.Id);
            if (book == null)
            {
                return Result.Failure<Book>("Entity not found");
            }

            var bookResult = book.SetName(request.Name);
            if (bookResult.IsFailure)
            {
                return Result.Failure<Book>(bookResult.Error);
            }

            this._repository.Update(bookResult.Value);
            await this._unitOfWork.SaveChangesAsync();

            return Result.Ok(bookResult.Value);
        }

        public async Task<Result<Book>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await this._repository.GetByIdAsync(request.Id);
            if (book == null)
            {
                return Result.Failure<Book>("Entity not found");
            }

            this._repository.Remove(book);
            await this._unitOfWork.SaveChangesAsync();

            return Result.Ok(book);
        }
    }
}
