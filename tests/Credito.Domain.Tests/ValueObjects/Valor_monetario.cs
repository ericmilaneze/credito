using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.Common.ValueObjects;
using FluentAssertions;
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

            valorMonetario.Should().BeEquivalentTo(new ValorMonetario(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_valor_monetario_de_decimal_implicitamente(decimal valor)
        {
            ValorMonetario valorMonetario = valor;

            valorMonetario.Should().BeEquivalentTo(new ValorMonetario(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Valor_monetario_para_decimal(decimal valor)
        {
            decimal valorMonetario = ValorMonetario.ToDecimal(ValorMonetario.FromDecimal(valor));

            valorMonetario.Should().Be(valor);
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_valores_monetarios_como_decimal(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<decimal>()
                {
                    ValorMonetario.ToDecimal(new ValorMonetario(valor1)),
                    ValorMonetario.ToDecimal(new ValorMonetario(valor2)),
                    ValorMonetario.ToDecimal(new ValorMonetario(valor3))
                };

            valores.Sum().Should().Be(ValorMonetario.ToDecimal(new ValorMonetario(resultado)));
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

            valores.Sum().Should().BeEquivalentTo(new ValorMonetario(resultado));
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

            valores.Min().Should().BeEquivalentTo(new ValorMonetario(resultado));
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
            
            valores.Max().Should().BeEquivalentTo(new ValorMonetario(resultado));
        }
    }
}