using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Credito.Domain.Tests.ValueObjects
{
    public class Valor_percentual_positivo_positivo
    {
        public static IEnumerable<object[]> DadosDeConversaoOuCriacao =>
            new List<object[]>
            {
                new object[] { 10.00M },
                new object[] { 49.97M }
            };

        public static IEnumerable<object[]> DadosDeConversaoOuCriacaoNegativos =>
            new List<object[]>
            {
                new object[] { -50.62M },
                new object[] { -71.97M }
            };

        public static IEnumerable<object[]> DadosParaAplicarPercentual =>
            new List<object[]>
            {
                new object[] { 100.00M, 25.20M, 25.20M },
                new object[] { 49.97M, 10.00M, 4.997M }
            };

        public static IEnumerable<object[]> ListaDeDadosSomatorio =>
            new List<object[]>
            {
                new object[] { 10.20M, 20.30M, 30.50M, 61.00M }
            };

        public static IEnumerable<object[]> ListaDeDadosMinimo =>
            new List<object[]>
            {
                new object[] { 10.20M, 20.30M, 30.50M, 10.20M }
            };

        public static IEnumerable<object[]> ListaDeDadosMaximo =>
            new List<object[]>
            {
                new object[] { 10.20M, 20.30M, 30.50M, 30.50M }
            };

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_percentual_positivo_de_decimal(decimal valor)
        {
            var percentual = PercentualPositivo.FromDecimal(valor);

            percentual.Should().BeEquivalentTo(new PercentualPositivo(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacaoNegativos))]
        public void Criar_percentual_positivo_de_decimal_negativo(decimal valor)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { var percentual = PercentualPositivo.FromDecimal(valor); });
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_percentual_positivo_de_decimal_implicitamente(decimal valor)
        {
            PercentualPositivo percentual = valor;

            percentual.Should().BeEquivalentTo(new PercentualPositivo(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacaoNegativos))]
        public void Criar_percentual_positivo_de_decimal_implicitamente_negativo(decimal valor)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { PercentualPositivo percentual = valor; });
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Percentual_positivo_para_decimal(decimal valor)
        {
            decimal percentual = PercentualPositivo.ToDecimal(PercentualPositivo.FromDecimal(valor));

            percentual.Should().Be(valor);
        }

        [Theory]
        [MemberData(nameof(DadosParaAplicarPercentual))]
        public void Aplicar_percentual_positivo(decimal valor, decimal percentual, decimal resultado)
        {
            PercentualPositivo.FromDecimal(percentual).Aplicar(valor)
                .Should().Be(resultado);
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_percentuais_como_decimal(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<decimal>()
                {
                    PercentualPositivo.ToDecimal(new PercentualPositivo(valor1)),
                    PercentualPositivo.ToDecimal(new PercentualPositivo(valor2)),
                    PercentualPositivo.ToDecimal(new PercentualPositivo(valor3))
                };

            valores.Sum().Should().Be(PercentualPositivo.ToDecimal(new PercentualPositivo(resultado)));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_percentuais(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<PercentualPositivo>()
                {
                    new PercentualPositivo(valor1),
                    new PercentualPositivo(valor2),
                    new PercentualPositivo(valor3)
                };

            valores.Sum().Should().BeEquivalentTo(new PercentualPositivo(resultado));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMinimo))]
        public void Minimo_de_percentuais(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<PercentualPositivo>()
                {
                    new PercentualPositivo(valor1),
                    new PercentualPositivo(valor2),
                    new PercentualPositivo(valor3)
                };

            valores.Min().Should().BeEquivalentTo(new PercentualPositivo(resultado));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMaximo))]
        public void Maximo_de_percentuais(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<PercentualPositivo>()
                {
                    new PercentualPositivo(valor1),
                    new PercentualPositivo(valor2),
                    new PercentualPositivo(valor3)
                };

            valores.Max().Should().BeEquivalentTo(new PercentualPositivo(resultado));
        }
    }
}