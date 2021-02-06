using Credito.Domain.ContratoDeEmprestimo;
using Credito.Domain.Tests.DataAttributes;
using Credito.Domain.Common.ValueObjects;
using Xunit;
using static Credito.Domain.ContratoDeEmprestimo.ContratoDeEmprestimoAggregate;
using System;
using FluentAssertions;
using System.Linq;

namespace Credito.Domain.Tests.ContratoDeEmprestimo
{
    public class Contrato_de_emprestimo
    {
        private const decimal PRECISION = 0.000000001M;

        [Theory]
        [ContratosDataAttribute("contratos.json")]
        public void Criar_contrato(ContratoData data)
        {
            var id = Guid.NewGuid();
            var contrato = ContratoDeEmprestimoAggregate.CriarContrato(
                new ParametrosDeContratoDeEmprestimo
                {
                    Id = id,
                    ValorLiquido = data.ValorLiquido,
                    QuantidadeDeParcelas = data.Prazo,
                    TaxaAoMes = data.TaxaAoMes,
                    Tac = data.Tac,
                    Iof = data.Iof,
                    DiasDeCarencia = data.DiasDeCarencia
                });

            contrato.Id.Should().Be(id);
            contrato.TaxaAoDia.Should().BeEquivalentTo(new Percentual(data.TaxaAoDia));
            contrato.ValorCarencia.Should().BeEquivalentTo(new ValorMonetario(data.ValorCarencia));
            contrato.ValorFinanciado.Should().BeEquivalentTo(new ValorMonetario(data.ValorFinanciado));
            ValorMonetario.ToDecimal(contrato.ValorDaParcela)
                          .Should()
                          .BeApproximately(data.ValorDaParcela, PRECISION);
            contrato.Parcelas.Should().HaveCount(data.Prazo);

            foreach (var parcela in contrato.Parcelas)
            {
                var parcelaExpected = data.Parcelas.Single(p => p.Numero == parcela.Numero);

                ValorMonetario.ToDecimal(parcela.SaldoDevedorInicial)
                              .Should()
                              .BeApproximately(ValorMonetario.ToDecimal(parcelaExpected.SaldoDevedorInicial), PRECISION);

                ValorMonetario.ToDecimal(parcela.Valor)
                              .Should()
                              .BeApproximately(ValorMonetario.ToDecimal(parcelaExpected.Valor), PRECISION);

                ValorMonetario.ToDecimal(parcela.Principal)
                              .Should()
                              .BeApproximately(ValorMonetario.ToDecimal(parcelaExpected.Principal), PRECISION);

                ValorMonetario.ToDecimal(parcela.Juros)
                              .Should()
                              .BeApproximately(ValorMonetario.ToDecimal(parcelaExpected.Juros), PRECISION);

                ValorMonetario.ToDecimal(parcela.SaldoDevedorFinal)
                              .Should()
                              .BeApproximately(ValorMonetario.ToDecimal(parcelaExpected.SaldoDevedorFinal), PRECISION);
            }
        }
    }
}