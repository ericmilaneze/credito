using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.ValueObjects;
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

            Assert.Equal(new Percentual(valor), percentual);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_percentual_de_decimal_implicitamente(decimal valor)
        {
            Percentual percentual = valor;

            Assert.Equal(new Percentual(valor), percentual);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Percentual_para_decimal(decimal valor)
        {
            decimal percentual = Percentual.FromDecimal(valor).ToDecimal();

            Assert.Equal(valor, percentual);
        }

        [Theory]
        [MemberData(nameof(DadosParaAplicarPercentual))]
        public void Aplicar_percentual(decimal valor, decimal percentual, decimal resultado)
        {
            Assert.Equal(resultado, Percentual.FromDecimal(percentual).Aplicar(valor));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_percentuais_como_decimal(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<decimal>()
                {
                    new Percentual(valor1).ToDecimal(),
                    new Percentual(valor2).ToDecimal(),
                    new Percentual(valor3).ToDecimal()
                };

            Assert.Equal(new Percentual(resultado), valores.Sum());
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

            Assert.Equal(new Percentual(resultado), valores.Sum());
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

            Assert.Equal(new Percentual(resultado), valores.Min());
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

            Assert.Equal(new Percentual(resultado), valores.Max());
        }
    }
}