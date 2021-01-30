using System;
using System.Diagnostics.CodeAnalysis;

namespace Credito.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        [ExcludeFromCodeCoverage]
        protected Entity() { }

        protected Entity(Guid id) =>
            Id = id;
    }
}