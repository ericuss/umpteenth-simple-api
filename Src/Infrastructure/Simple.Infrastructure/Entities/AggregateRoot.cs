// Copyright (c) Simple. All rights reserved.
namespace Simple.Infrastructure.Entities
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        protected AggregateRoot()
            : base()
        {
        }

        protected AggregateRoot(TKey id)
            : base(id)
        {
        }
    }
}