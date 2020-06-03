// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.Repository
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}