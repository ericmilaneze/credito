using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.Common.ValueObjects;
using Xunit;

namespace Credito.Domain.Tests.ValueObjects
{
    public class Valor_monetario
    {
        public static IEnumerable<object[]> DadosDeConversaoOuCriacao =>
            new List<object[]>
            {
                new object[] { 10.00M },
                new object[] { 49.97M },
                new object[] { -50.62M },
                new object[] { -71.97M }
            };

        public static IEnumerable<object[]> ListaDeDadosSomatorio =>
            new List<object[]>
            {
                new object[] { 10.20M, 20.30M, 30.50M, 61.00M },
                new object[] { 10.20M, 20.30M, -30.50M, 0.00M },
                new object[] { 10.20M, 20.30M, -40.50M, -10.00M }
            };

        public static IEnumerable<object[]> ListaDeDadosMinimo =>
            new List<object[]>
            {
                new object[] { 10.20M, 20.30M, 30.50M, 10.20M },
                new object[] { 10.20M, 20.30M, -30.50M, -30.50M },
                new object[] { 10.20M, 20.30M, -40.50M, -40.50M }
            };

        public static IEnumerable<object[]> ListaDeDadosMaximo =>
            new List<object[]>
            {
                new object[] { 10.20M, 20.30M, 30.50M, 30.50M },
                new object[] { 10.20M, 20.30M, -30.50M, 20.30M },
                new object[] { 10.20M, 20.30M, -40.50M, 20.30M }
            };

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_valor_monetario_de_decimal(decimal valor)
        {
            var valorMonetario = ValorMonetario.FromDecimal(valor);

            Assert.Equal(new ValorMonetario(valor), valorMonetario);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_valor_monetario_de_decimal_implicitamente(decimal valor)
        {
            ValorMonetario valorMonetario = valor;

            Assert.Equal(new ValorMonetario(valor), valorMonetario);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Valor_monetario_para_decimal(decimal valor)
        {
            decimal valorMonetario = ValorMonetario.FromDecimal(valor).ToDecimal();

            Assert.Equal(valor, valorMonetario);
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_valores_monetarios_como_decimal(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<decimal>()
                {
                    new ValorMonetario(valor1).ToDecimal(),
                    new ValorMonetario(valor2).ToDecimal(),
                    new ValorMonetario(valor3).ToDecimal()
                };

            Assert.Equal(new ValorMonetario(resultado), valores.Sum());
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_valores_monetarios(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<ValorMonetario>()
                {
                    new ValorMonetario(valor1),
                    new ValorMonetario(valor2),
                    new ValorMonetario(valor3)
                };

            Assert.Equal(new ValorMonetario(resultado), valores.Sum());
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMinimo))]
        public void Minimo_de_valores_monetarios(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<ValorMonetario>()
                {
                    new ValorMonetario(valor1),
                    new ValorMonetario(valor2),
                    new ValorMonetario(valor3)
                };

            Assert.Equal(new ValorMonetario(resultado), valores.Min());
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMaximo))]
        public void Maximo_de_valores_monetarios(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<ValorMonetario>()
                {
                    new ValorMonetario(valor1),
                    new ValorMonetario(valor2),
                    new ValorMonetario(valor3)
                };

            Assert.Equal(new ValorMonetario(resultado), valores.Max());
        }
    }
}