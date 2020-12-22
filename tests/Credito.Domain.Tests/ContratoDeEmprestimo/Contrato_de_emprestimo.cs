using Credito.Domain.ContratoDeEmprestimo;
using Credito.Domain.ValueObjects;
using Xunit;
using static Credito.Domain.ContratoDeEmprestimo.ContratoDeEmprestimoAggragateRoot;

namespace Credito.Domain.Tests.ContratoDeEmprestimo
{
    public class Contrato_de_emprestimo
    {
        [Fact]
        public void Criar_contrato()
        {
            var contrato = ContratoDeEmprestimoAggragateRoot.CriarContrato(
                new ParametrosDeContratoDeEmprestimo
                {
                    ValorLiquido = 3000M,
                    Prazo = 24,
                    TaxaAoMes = 5.00M,
                    Tac = 6.00M,
                    Iof = 10.00M,
                    DiasDeCarencia = 30
                });

            Assert.Equal(new Percentual(0.16276620118331753M), contrato.TaxaAoDia);
            Assert.Equal(new ValorMonetario(146.4895810649857770000M), contrato.ValorCarencia);
            Assert.Equal(new ValorMonetario(3190.00M), contrato.ValorFinanciado);
            Assert.Equal(new ValorMonetario(231.18217340107145820781457680M), contrato.ValorDaParcela);
        }
    }
}