using System;

namespace Credito.Domain.Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; private set; }

        protected AggregateRoot() { }

        public AggregateRoot(Guid id)
        {
            Id = id;
        }
    }
}