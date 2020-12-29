using System;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB.Example;

namespace Credito.Framework.MongoDB.Registry
{
    public class CollectionNamesRegistry
    {
        public static readonly string ExampleAggregate = "example_aggregates";
        public static readonly string ContratoDeEmprestimoAggregate = "contratos";

        public static string GetCollectionName<T>()
        {
            switch (typeof(T))
            {
                case Type t when t == typeof(ExampleAggregate):
                    return ExampleAggregate;
                case Type t when t == typeof(ContratoDeEmprestimoAggregate):
                    return ContratoDeEmprestimoAggregate;
                default:
                    throw new NotImplementedException($"Collection name not set yet for \"{typeof(T).Name}\"");
            }
        }
    }
}