namespace Credito.Framework.MongoDB.Tests.Models
{
    public record Pessoa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}