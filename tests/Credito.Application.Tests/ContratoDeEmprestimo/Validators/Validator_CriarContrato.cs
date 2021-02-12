using System;
using Credito.Application.v1_0.ContratoDeEmprestimo.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Credito.Application.Tests.ContratoDeEmprestimo.Validators
{
    public class Validator_CriarContrato
    {
        private CriarContratoValidator _validator;

        public Validator_CriarContrato()
        {
            _validator = new CriarContratoValidator();
        }

        [Fact]
        public void Validator_CriarContratoCmd_ValorLiquido_Zero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.ValorLiquido, 0M);

        [Fact]
        public void Validator_CriarContratoCmd_ValorLiquido_MenorQueZero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.ValorLiquido, -10M);

        [Fact]
        public void Validator_CriarContratoCmd_ValorLiquido_MaiorQueZero() =>
            _validator.ShouldNotHaveValidationErrorFor(cmd => cmd.ValorLiquido, 10M);

        [Fact]
        public void Validator_CriarContratoCmd_QuantidadeDeParcelas_Zero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.QuantidadeDeParcelas, 0);

        [Fact]
        public void Validator_CriarContratoCmd_QuantidadeDeParcelas_MenorQueZero() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.QuantidadeDeParcelas, -10);

        [Fact]
        public void Validator_CriarContratoCmd_QuantidadeDeParcelas_MaiorQueZero() =>
            _validator.ShouldNotHaveValidationErrorFor(cmd => cmd.QuantidadeDeParcelas, 10);

        [Fact]
        public void Validator_CriarContratoCmd_Id_Null() =>
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.Id, Guid.Empty);

        [Fact]
        public void Validator_CriarContratoCmd_Id() =>
            _validator.ShouldNotHaveValidationErrorFor(cmd => cmd.Id, Guid.NewGuid());
    }
}