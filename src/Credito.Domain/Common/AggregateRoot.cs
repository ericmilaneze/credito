using System;

namespace Credito.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot()
        { }

        protected AggregateRoot(Guid id)
            : base(id)
        {
            
        }
    }
}