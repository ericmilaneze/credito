using Credito.Application.v1_0.ContratoDeEmprestimo.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Credito.Application.Tests.ContratoDeEmprestimo.Validators
{
    public class Validator_CalcularContrato
    {
        private CalcularContratoValidator _validator;

        public Validator_CalcularContrato()
        {
            _validator = new CalcularContratoValidator();
        }

        [Fact]
        public void Validator_CalcularContratoCmd_ValorLiquido_Zero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.ValorLiquido, 0M);

        [Fact]
        public void Validator_CalcularContratoCmd_ValorLiquido_MenorQueZero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.ValorLiquido, -10M);

        [Fact]
        public void Validator_CalcularContratoCmd_ValorLiquido_MaiorQueZero() =>
            _validator.ShouldNotHaveValidationErrorFor(cmd => cmd.ValorLiquido, 10M);

        [Fact]
        public void Validator_CalcularContratoCmd_QuantidadeDeParcelas_Zero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.QuantidadeDeParcelas, 0);

        [Fact]
        public void Validator_CalcularContratoCmd_QuantidadeDeParcelas_MenorQueZero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.QuantidadeDeParcelas, -10);

        [Fact]
        public void Validator_CalcularContratoCmd_QuantidadeDeParcelas_MaiorQueZero() =>
            _validator.ShouldNotHaveValidationErrorFor(cmd => cmd.QuantidadeDeParcelas, 10);
    }
}