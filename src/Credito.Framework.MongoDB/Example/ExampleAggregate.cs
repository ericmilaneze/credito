using System;
using Credito.Domain.Common;

namespace Credito.Framework.MongoDB.Example
{
    public class ExampleAggregate : AggregateRoot
    {
        public string Nome { get; private set; }
        public IdadeExample Idade { get; private set; }
        public decimal DinheiroNaConta { get; private set; }

        private ExampleAggregate() { }

        private ExampleAggregate(Guid id,
                                 string nome,
                                 IdadeExample idade,
                                 decimal dinheiroNaConta) : base(id)
        {
            Nome = nome;
            Idade = idade;
            DinheiroNaConta = dinheiroNaConta;
        }

        public static ExampleAggregate CreateExampleAggregate(Guid id,
                                                              string nome,
                                                              IdadeExample idade,
                                                              decimal dinheiroNaConta) =>
            new ExampleAggregate(id,
                                 nome,
                                 idade,
                                 dinheiroNaConta);
    }
}