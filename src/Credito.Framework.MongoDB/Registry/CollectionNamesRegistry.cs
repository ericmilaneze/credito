using System;
using System.Collections.Generic;
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
            Dictionary<Type, string> collectionNames = new Dictionary<Type, string>()
            {
                [typeof(ExampleAggregate)] = ExampleAggregate,
                [typeof(ContratoDeEmprestimoAggregate)] = ContratoDeEmprestimoAggregate
            };

            return collectionNames[typeof(T)];
        }
    }
}