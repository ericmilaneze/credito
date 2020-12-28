using System;
using Credito.Domain.Common;

namespace Credito.Framework.MongoDB
{
    public class Aggregate : AggregateRoot
    {
        public string Nome { get; }
        public Idade Idade { get; }
        public decimal DinheiroNaConta { get; }

        public Aggregate(Guid id,
                         string nome,
                         Idade idade,
                         decimal dinheiroNaConta) : base(id)
        {
            Nome = nome;
            Idade = idade;
            DinheiroNaConta = dinheiroNaConta;
        }
    }
}