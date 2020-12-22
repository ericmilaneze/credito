using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Credito.Domain.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo
{
    public class ContratoDeEmprestimoAggragateRoot
    {
        private ICollection<Parcela> parcelas = new Collection<Parcela>();

        public string Id { get; }
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

        private ContratoDeEmprestimoAggragateRoot(ParametrosDeContratoDeEmprestimo parametros)
        {
            ValorLiquido = parametros.ValorLiquido;
            QuantidadeDeParcelas = parametros.Prazo;
            TaxaAoMes = parametros.TaxaAoMes;
            Tac = parametros.Tac;
            Iof = parametros.Iof;
            DiasDeCarencia = parametros.DiasDeCarencia;
        }

        public static ContratoDeEmprestimoAggragateRoot CriarContrato(ParametrosDeContratoDeEmprestimo parametros)
        {
            var contrato = new ContratoDeEmprestimoAggragateRoot(parametros);
            contrato.GerarParcelas();
            return contrato;
        }

        public void GerarParcelas()
        {
            for (int i = 1; i <= QuantidadeDeParcelas.ToInt(); i++)
                AdicionarParcela(i);
        }

        private void AdicionarParcela(NumeroParcela numeroDaParcela)
        {
            var isPrimeiraParcela = numeroDaParcela == 1;
            var saldoDevedorInicial =
                isPrimeiraParcela
                    ? ValorCarencia + ValorFinanciado
                    : ObterParcela(numeroDaParcela - 1).SaldoDevedorFinal;
            var juros =
                isPrimeiraParcela
                    ? TaxaAoMes.Aplicar(ValorFinanciado)
                    : TaxaAoMes.Aplicar(saldoDevedorInicial);
            var principal =
                isPrimeiraParcela
                    ? ValorDaParcela - juros + ValorCarencia
                    : ValorDaParcela - juros;

            parcelas.Add(new Parcela
            {
                Numero = numeroDaParcela,
                SaldoDevedorInicial = saldoDevedorInicial,
                Valor = ValorDaParcela,
                Principal = principal,
                Juros = juros,
            });
        }

        public Parcela ObterParcela(NumeroParcela numero) =>
            Parcelas.First(p => p.Numero == numero);

        public record ParametrosDeContratoDeEmprestimo
        {
            public string Id { get; init; }
            public ValorMonetarioPositivo ValorLiquido { get; init; }
            public Prazo Prazo { get; init; }
            public PercentualPositivo TaxaAoMes { get; init; }
            public Percentual Tac { get; init; }
            public ValorMonetario Iof { get; init; }
            public Prazo DiasDeCarencia { get; init; }
        }
    }
}