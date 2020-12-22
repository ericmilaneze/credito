using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.ValueObjects;
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
            int prazo = Prazo.FromInt(valor).ToInt();

            Assert.Equal(valor, prazo);
        }

        [Theory]
        [MemberData(nameof(ListaDeDadosSomatorio))]
        public void Somatorio_de_prazo_em_dias_como_int(int valor1, int valor2, int valor3, int resultado)
        {
            var valores = 
                new Collection<int>()
                {
                    new Prazo(valor1).ToInt(),
                    new Prazo(valor2).ToInt(),
                    new Prazo(valor3).ToInt()
                };

            Assert.Equal(new Prazo(resultado), valores.Sum());
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

            Assert.Equal(new Prazo(resultado), valores.Sum());
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

            Assert.Equal(new Prazo(resultado), valores.Min());
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

            Assert.Equal(new Prazo(resultado), valores.Max());
        }
    }
}