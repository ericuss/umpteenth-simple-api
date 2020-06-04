namespace Simple.Infrastructure.Entities
{
    using System;

    public interface IModificationDate
    {
        public DateTimeOffset Modified { get; }
    }
}
