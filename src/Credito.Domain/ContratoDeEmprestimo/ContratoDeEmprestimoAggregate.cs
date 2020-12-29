using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.Common;
using Credito.Domain.Common.ValueObjects;
using Credito.Domain.ContratoDeEmprestimo.CalculadoraDeParcela;

namespace Credito.Domain.ContratoDeEmprestimo
{
    public class ContratoDeEmprestimoAggregate : AggregateRoot
    {
        private ICollection<Parcela> _parcelas = new Collection<Parcela>();

        public ValorMonetarioPositivo ValorLiquido { get; private set; }
        public Prazo QuantidadeDeParcelas { get; private set; }
        public PercentualPositivo TaxaAoMes { get; private set; }
        public Percentual Tac { get; private set; }
        public ValorMonetario Iof { get; private set; }
        public Prazo DiasDeCarencia { get; private set; }
        public IReadOnlyCollection<Parcela> Parcelas => _parcelas.ToList().AsReadOnly();

        public Percentual TaxaAoDia =>
            (Math.Pow(decimal.ToDouble(1M + TaxaAoMes), 1D / 30D) - 1) * 100;

        public ValorMonetario ValorCarencia =>
            TaxaAoDia.Aplicar(DiasDeCarencia) * ValorLiquido;

        public ValorMonetario ValorFinanciado =>
            Tac.Aplicar(ValorLiquido) + ValorLiquido + Iof;

        public ValorMonetario ValorDaParcela
        {
            get
            {
                var denominator = Math.Pow(decimal.ToDouble(1 + TaxaAoMes),
                                           decimal.ToDouble(QuantidadeDeParcelas.ToInt()))
                                  - 1;
                return (TaxaAoMes + (TaxaAoMes/denominator)) * ValorFinanciado;
            }
        }

        private ContratoDeEmprestimoAggregate() { }

        private ContratoDeEmprestimoAggregate(ParametrosDeContratoDeEmprestimo parametros)
            : base(parametros.Id)
        {
            ValorLiquido = parametros.ValorLiquido;
            QuantidadeDeParcelas = parametros.Prazo;
            TaxaAoMes = parametros.TaxaAoMes;
            Tac = parametros.Tac;
            Iof = parametros.Iof;
            DiasDeCarencia = parametros.DiasDeCarencia;
        }

        public static ContratoDeEmprestimoAggregate CriarContrato(ParametrosDeContratoDeEmprestimo parametros)
        {
            var contrato = new ContratoDeEmprestimoAggregate(parametros);
            contrato.GerarParcelas();
            return contrato;
        }

        private void GerarParcelas()
        {
            for (int numeroParcela = 1; numeroParcela <= QuantidadeDeParcelas.ToInt(); numeroParcela++)
                AdicionarParcela(numeroParcela, CalculadoraDeParcelaFactory.Create(numeroParcela));
        }

        private void AdicionarParcela(NumeroParcela numeroParcela, ICalculadoraDeParcelaStrategy calculadora) =>
            _parcelas.Add(new Parcela
            {
                Numero = numeroParcela,
                SaldoDevedorInicial = calculadora.CalcularSaldoDevedorInicial(this, numeroParcela),
                Valor = ValorDaParcela,
                Principal = calculadora.CalcularPrincipal(this, numeroParcela),
                Juros = calculadora.CalcularJuros(this, numeroParcela),
            });

        internal Parcela ObterParcela(NumeroParcela numero) =>
            Parcelas.First(p => p.Numero == numero);

        public record ParametrosDeContratoDeEmprestimo
        {
            public Guid Id { get; init; }
            public ValorMonetarioPositivo ValorLiquido { get; init; }
            public Prazo Prazo { get; init; }
            public PercentualPositivo TaxaAoMes { get; init; }
            public Percentual Tac { get; init; }
            public ValorMonetario Iof { get; init; }
            public Prazo DiasDeCarencia { get; init; }
        }
    }
}