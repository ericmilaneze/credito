using Credito.Application.v1_0.ContratoDeEmprestimo.Commands;
using FluentValidation;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.Validators
{
    public class CalcularContratoValidator : AbstractValidator<CalcularContratoCmd>
    {
        public CalcularContratoValidator()
        {
            RuleFor(x => x.ValorLiquido)
                .NotEmpty()
                .GreaterThan(0M)
                .WithMessage("{PropertyName} é obrigatório e deve ser maior que zero.");

            RuleFor(x => x.QuantidadeDeParcelas)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("{PropertyName} é obrigatória e deve ser maior que zero.");
        }
    }
}