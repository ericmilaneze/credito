using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.ValueObjects;
using Xunit;

namespace Credito.Domain.Tests.ValueObjects
{
    public class Valor_monetario_positivo
    {
        public static IEnumerable<object[]> DadosDeConversaoOuCriacao =>
            new List<object[]>
            {
                new object[] { 10.00M },
                new object[] { 49.97M }
            };

        public static IEnumerable<object[]> DadosDeConversaoOuCriacaoNegativo =>
            new List<object[]>
            {
                new object[] { -50.62M },
                new object[] { -71.97M }
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
        public void Criar_valor_monetario_positivo_de_decimal(decimal valor)
        {
            var valorMonetario = ValorMonetarioPositivo.FromDecimal(valor);

            Assert.Equal(new ValorMonetarioPositivo(valor), valorMonetario);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_valor_monetario_positivo_de_decimal_implicitamente(decimal valor)
        {
            ValorMonetarioPositivo valorMonetario = valor;

            Assert.Equal(new ValorMonetarioPositivo(valor), valorMonetario);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacaoNegativo))]
        public void Criar_valor_monetario_positivo_de_decimal_negativo(decimal valor)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ValorMonetarioPositivo.FromDecimal(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacaoNegativo))]
        public void Criar_valor_monetario_positivo_de_decimal_implicitamente_negativo(decimal valor)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { ValorMonetarioPositivo valorMonetario = valor; });
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Valor_monetario_positivo_para_decimal(decimal valor)
        {
            decimal valorMonetario = ValorMonetarioPositivo.FromDecimal(valor).ToDecimal();

            Assert.Equal(valor, valorMonetario);
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_valores_monetarios_positivos_como_decimal(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<decimal>()
                {
                    new ValorMonetarioPositivo(valor1).ToDecimal(),
                    new ValorMonetarioPositivo(valor2).ToDecimal(),
                    new ValorMonetarioPositivo(valor3).ToDecimal()
                };

            Assert.Equal(new ValorMonetarioPositivo(resultado), valores.Sum());
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_valores_monetarios_positivos(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<ValorMonetarioPositivo>()
                {
                    new ValorMonetarioPositivo(valor1),
                    new ValorMonetarioPositivo(valor2),
                    new ValorMonetarioPositivo(valor3)
                };

            Assert.Equal(new ValorMonetarioPositivo(resultado), valores.Sum());
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMinimo))]
        public void Minimo_de_valores_monetarios_positivos(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<ValorMonetarioPositivo>()
                {
                    new ValorMonetarioPositivo(valor1),
                    new ValorMonetarioPositivo(valor2),
                    new ValorMonetarioPositivo(valor3)
                };

            Assert.Equal(new ValorMonetarioPositivo(resultado), valores.Min());
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMaximo))]
        public void Maximo_de_valores_monetarios_positivos(decimal valor1, decimal valor2, decimal valor3, decimal resultado)
        {
            var valores = 
                new Collection<ValorMonetarioPositivo>()
                {
                    new ValorMonetarioPositivo(valor1),
                    new ValorMonetarioPositivo(valor2),
                    new ValorMonetarioPositivo(valor3)
                };

            Assert.Equal(new ValorMonetarioPositivo(resultado), valores.Max());
        }
    }
}