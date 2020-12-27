using System;

namespace Credito.Framework.MongoDB
{
    public class Aggregate
    {
        public Guid Id { get; }
        public string Nome { get; }
        public Idade Idade { get; }
        public decimal DinheiroNaConta { get; }

        public Aggregate(Guid id, string nome, Idade idade, decimal dinheiroNaConta)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            DinheiroNaConta = dinheiroNaConta;
        }
    }
}