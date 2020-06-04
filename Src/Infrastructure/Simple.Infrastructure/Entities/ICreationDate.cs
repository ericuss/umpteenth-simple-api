namespace Simple.Infrastructure.Entities
{
    using System;

    public interface ICreationDate
    {
        public DateTimeOffset Created { get; }
    }
}
