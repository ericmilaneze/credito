using System;

namespace Credito.Domain.Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }

        public AggregateRoot(Guid id)
        {
            Id = id;
        }
    }
}