// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}