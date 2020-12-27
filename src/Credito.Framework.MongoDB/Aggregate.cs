namespace Credito.Framework.MongoDB
{
    public class Aggregate
    {
        public string Id { get; }
        public string Nome { get; }
        public Idade Idade { get; }
        public decimal DinheiroNaConta { get; }

        public Aggregate(string id, string nome, Idade idade, decimal dinheiroNaConta)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            DinheiroNaConta = dinheiroNaConta;
        }
    }
}