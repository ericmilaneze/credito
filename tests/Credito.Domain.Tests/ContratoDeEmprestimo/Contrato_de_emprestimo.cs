using Credito.Domain.ContratoDeEmprestimo;
using Credito.Domain.Tests.DataAttributes;
using Credito.Domain.Common.ValueObjects;
using Xunit;
using static Credito.Domain.ContratoDeEmprestimo.ContratoDeEmprestimoAggragate;

namespace Credito.Domain.Tests.ContratoDeEmprestimo
{
    public class Contrato_de_emprestimo
    {
        [Theory]
        [ContratosDataAttribute("contratos.json")]
        public void Criar_contrato(ContratoData data)
        {
            var contrato = ContratoDeEmprestimoAggragate.CriarContrato(
                new ParametrosDeContratoDeEmprestimo
                {
                    ValorLiquido = data.ValorLiquido,
                    Prazo = data.Prazo,
                    TaxaAoMes = data.TaxaAoMes,
                    Tac = data.Tac,
                    Iof = data.Iof,
                    DiasDeCarencia = data.DiasDeCarencia
                });

            Assert.Equal(new Percentual(data.TaxaAoDia), contrato.TaxaAoDia);
            Assert.Equal(new ValorMonetario(data.ValorCarencia), contrato.ValorCarencia);
            Assert.Equal(new ValorMonetario(data.ValorFinanciado), contrato.ValorFinanciado);
            Assert.Equal(new ValorMonetario(data.ValorDaParcela), contrato.ValorDaParcela);
            Assert.All(contrato.Parcelas, p => data.Parcelas.Contains(p));
        }
    }
}