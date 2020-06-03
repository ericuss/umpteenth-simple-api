// Copyright (c) simple. All rights reserved.

namespace Simple.Data.Repositories.Core
{
    using System.Threading.Tasks;
    using Simple.Data.Contexts;
    using Simple.Infrastructure.Repository;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;

        public UnitOfWork(LibraryContext context)
        {
            this._context = context;
        }

        public Task<int> SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }
    }
}