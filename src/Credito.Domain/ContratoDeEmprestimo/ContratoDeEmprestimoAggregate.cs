using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.Common.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo
{
    public class ContratoDeEmprestimoAggregate
    {
        private ICollection<Parcela> parcelas = new Collection<Parcela>();

        public Guid Id { get; } = Guid.NewGuid();
        public ValorMonetarioPositivo ValorLiquido { get; }
        public Prazo QuantidadeDeParcelas { get; }
        public PercentualPositivo TaxaAoMes { get; }
        public Percentual Tac { get; }
        public ValorMonetario Iof { get; }
        public Prazo DiasDeCarencia { get; }
        public IReadOnlyCollection<Parcela> Parcelas => parcelas.ToList().AsReadOnly();

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

        private ContratoDeEmprestimoAggregate(ParametrosDeContratoDeEmprestimo parametros)
        {
            Id = parametros.Id;
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
                AdicionarParcela(numeroParcela);
        }

        private void AdicionarParcela(NumeroParcela numeroDaParcela)
        {
            parcelas.Add(new Parcela
            {
                Numero = numeroDaParcela,
                SaldoDevedorInicial = CalcularSaldoDevedorInicial(),
                Valor = ValorDaParcela,
                Principal = CalcularPrincipal(),
                Juros = CalcularJuros(),
            });

            ValorMonetario CalcularSaldoDevedorInicial()
            {
                return numeroDaParcela.IsFirst()
                    ? ValorCarencia + ValorFinanciado
                    : ObterParcela(numeroDaParcela - 1).SaldoDevedorFinal;
            }

            ValorMonetario CalcularJuros()
            {
                return numeroDaParcela.IsFirst()
                    ? TaxaAoMes.Aplicar(ValorFinanciado)
                    : TaxaAoMes.Aplicar(CalcularSaldoDevedorInicial());
            }

            decimal CalcularPrincipal()
            {
                return numeroDaParcela.IsFirst()
                    ? ValorDaParcela - CalcularJuros() + ValorCarencia
                    : ValorDaParcela - CalcularJuros();
            }
        }

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