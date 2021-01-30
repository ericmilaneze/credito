using System;
using System.Diagnostics.CodeAnalysis;

namespace Credito.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {
        [ExcludeFromCodeCoverage]
        protected AggregateRoot()
        { }

        protected AggregateRoot(Guid id)
            : base(id)
        {
            
        }
    }
}