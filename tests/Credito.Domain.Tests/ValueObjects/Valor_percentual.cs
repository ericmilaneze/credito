using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Credito.Domain.Tests.ValueObjects
{
    public class Valor_percentual
    {
        public static IEnumerable<object[]> DadosDeConversaoOuCriacao =>
            new List<object[]>
            {
                new object[] { 10.00M },
                new object[] { 49.97M },
                new object[] { -50.62M },
                new object[] { -71.97M }
            };

        public static IEnumerable<object[]> DadosIntParaAplicarPercentual =>
            new List<object[]>
            {
                new object[] { 100, 25.20M, 25.20M },
                new object[] { 50, 10.00M, 5M }
            };

        public static IEnumerable<object[]> DadosParaAplicarPercentual =>
            new List<object[]>
            {
                new object[] { 100.00M, 25.20M, 25.20M },
                new object[] { 49.97M, 10.00M, 4.997M },
                new object[] { -50.62M, 10.00M, -5.062M },
                new object[] { -71.97M, -10.00M, 7.197M }
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
        public void Criar_percentual_de_decimal(decimal valor)
        {
            var percentual = Percentual.FromDecimal(valor);

            percentual.Should().BeEquivalentTo(new Percentual(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_percentual_de_decimal_implicitamente(decimal valor)
        {
            Percentual percentual = valor;

            percentual.Should().BeEquivalentTo(new Percentual(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Percentual_para_decimal(decimal valor)
        {
            decimal percentual = Percentual.ToDecimal(Percentual.FromDecimal(valor));

            percentual.Should().Be(valor);
        }

        [Theory]
        [MemberData(nameof(DadosParaAplicarPercentual))]
        public void Aplicar_percentual(decimal valor, decimal percentual, decimal resultado)
        {
            Percentual.FromDecimal(percentual).Aplicar(valor)
                .Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(DadosParaAplicarPercentual))]
        public void Aplicar_percentual_multiply_operator(decimal valor, decimal percentual, decimal resultado)
        {
            var res = Percentual.FromDecimal(percentual) * valor;
            res.Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(DadosParaAplicarPercentual))]
        public void Aplicar_percentual_value_from_decimal(decimal valor, decimal percentual, decimal resultado)
        {
            var valueFromDecimal = Percentual.FromDecimal(valor);
            Percentual.FromDecimal(percentual).Aplicar(valueFromDecimal)
                .Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(DadosParaAplicarPercentual))]
        public void Aplicar_percentual_value_from_decimal_multiply_operator(decimal valor, decimal percentual, decimal resultado)
        {
            var valueFromDecimal = Percentual.FromDecimal(valor);
            var res = Percentual.FromDecimal(percentual) * valueFromDecimal;
            res.Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(DadosIntParaAplicarPercentual))]
        public void Aplicar_percentual_valor_int(int valor, decimal percentual, decimal resultado)
        {
            Percentual.FromDecimal(percentual).Aplicar(valor)
                .Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(DadosIntParaAplicarPercentual))]
        public void Aplicar_percentual_valor_int_multiply_operator(int valor, decimal percentual, decimal resultado)
        {
            var res = Percentual.FromDecimal(percentual) * valor;
            res.Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(DadosIntParaAplicarPercentual))]
        public void Aplicar_percentual_value_from_int_multiply_operator(int valor, decimal percentual, decimal resultado)
        {
            var res = Percentual.FromDecimal(percentual) * Prazo.FromInt(valor);
            res.Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_percentuais_como_decimal(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<decimal>()
                {
                    Percentual.ToDecimal(new Percentual(valor1)),
                    Percentual.ToDecimal(new Percentual(valor2)),
                    Percentual.ToDecimal(new Percentual(valor3))
                };

            valores.Sum().Should().Be(Percentual.ToDecimal(new Percentual(resultado)));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_percentuais(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<Percentual>()
                {
                    new Percentual(valor1),
                    new Percentual(valor2),
                    new Percentual(valor3)
                };

            valores.Sum().Should().BeEquivalentTo(new Percentual(resultado));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMinimo))]
        public void Minimo_de_percentuais(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<Percentual>()
                {
                    new Percentual(valor1),
                    new Percentual(valor2),
                    new Percentual(valor3)
                };

            valores.Min().Should().BeEquivalentTo(new Percentual(resultado));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMaximo))]
        public void Maximo_de_percentuais(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<Percentual>()
                {
                    new Percentual(valor1),
                    new Percentual(valor2),
                    new Percentual(valor3)
                };

            valores.Max().Should().BeEquivalentTo(new Percentual(resultado));
        }
    }
}