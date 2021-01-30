using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Credito.Domain.Tests.ValueObjects
{
    public class Prazo_em_dias
    {
        public static IEnumerable<object[]> DadosDeConversaoOuCriacao =>
            new List<object[]>
            {
                new object[] { 10 },
                new object[] { 49 }
            };

        public static IEnumerable<object[]> DadosDeConversaoOuCriacaoNegativo =>
            new List<object[]>
            {
                new object[] { -50 },
                new object[] { -71 }
            };

        public static IEnumerable<object[]> ListaDeDadosSomatorio =>
            new List<object[]>
            {
                new object[] { 10, 20, 30, 60 }
            };

        public static IEnumerable<object[]> ListaDeDadosMinimo =>
            new List<object[]>
            {
                new object[] { 10, 20, 30, 10 }
            };

        public static IEnumerable<object[]> ListaDeDadosMaximo =>
            new List<object[]>
            {
                new object[] { 10, 20, 30, 30 }
            };

        public static IEnumerable<object[]> Subtracao =>
            new List<object[]>
            {
                new object[] { 10, 5, 5 },
                new object[] { 49, 7, 42 }
            };

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_prazo_em_dias_de_int(int valor)
        {
            var prazo = Prazo.FromInt(valor);

            Assert.Equal(new Prazo(valor), prazo);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Criar_prazo_em_dias_de_int_implicitamente(int valor)
        {
            Prazo prazo = valor;

            Assert.Equal(new Prazo(valor), prazo);
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacaoNegativo))]
        public void Criar_prazo_em_dias_de_int_negativo(int valor)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Prazo.FromInt(valor));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacaoNegativo))]
        public void Criar_prazo_em_dias_de_int_implicitamente_negativo(int valor)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { ValorMonetarioPositivo prazo = valor; });
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Prazo_em_dias_para_int(int valor)
        {
            int prazo = Prazo.ToInt(Prazo.FromInt(valor));

            Assert.Equal(valor, prazo);
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_prazo_em_dias_como_int(int valor1, int valor2, int valor3, int resultado)
        {
            var valores = 
                new Collection<int>()
                {
                    Prazo.ToInt(new Prazo(valor1)),
                    Prazo.ToInt(new Prazo(valor2)),
                    Prazo.ToInt(new Prazo(valor3))
                };

            valores.Sum().Should().Be(Prazo.ToInt(new Prazo(resultado)));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_prazo_em_dias(int valor1, int valor2, int valor3, int resultado)
        {
            var valores = 
                new Collection<Prazo>()
                {
                    new Prazo(valor1),
                    new Prazo(valor2),
                    new Prazo(valor3)
                };

            valores.Sum().Should().BeEquivalentTo(new Prazo(resultado));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMinimo))]
        public void Minimo_de_prazo_em_dias(int valor1, int valor2, int valor3, int resultado)
        {
            var valores = 
                new Collection<Prazo>()
                {
                    new Prazo(valor1),
                    new Prazo(valor2),
                    new Prazo(valor3)
                };

            valores.Min().Should().BeEquivalentTo(new Prazo(resultado));
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosMaximo))]
        public void Maximo_de_prazo_em_dias(int valor1, int valor2, int valor3, int resultado)
        {
            var valores = 
                new Collection<Prazo>()
                {
                    new Prazo(valor1),
                    new Prazo(valor2),
                    new Prazo(valor3)
                };

            valores.Max().Should().BeEquivalentTo(new Prazo(resultado));
        }

        [Theory]
        [MemberData(nameof(DadosDeConversaoOuCriacao))]
        public void Prazo_to_int_nullable(int valor)
        {
            var prazo = Prazo.FromInt(valor);
            int? prazoAsIntNullable = ValueFromInt.ToIntNullable(prazo);

            prazoAsIntNullable.Should().Be(prazoAsIntNullable);
        }

        [Theory]
        [MemberData(nameof(Subtracao))]
        public void Prazo_subtract_operator(int valor1, int valor2, int resultado)
        {
            var prazo1 = Prazo.FromInt(valor1);
            var prazo2 = Prazo.FromInt(valor2);
            var res = prazo1 - prazo2;
            res.Should().Be(resultado);
        }
    }
}