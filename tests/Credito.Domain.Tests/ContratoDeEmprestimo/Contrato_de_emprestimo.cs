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
            contrato.ValorDaParcela.Should().BeEquivalentTo(new ValorMonetario(data.ValorDaParcela));
            contrato.Parcelas.All(p => data.Parcelas.Contains(p));
        }
    }
}