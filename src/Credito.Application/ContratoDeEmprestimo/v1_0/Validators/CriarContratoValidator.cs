using Credito.Application.v1_0.ContratoDeEmprestimo.Commands;
using FluentValidation;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.Validators
{
    public class CriarContratoValidator : AbstractValidator<CriarContratoCmd>
    {
        public CriarContratoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} é obrigatório.");

            RuleFor(x => x.ValorLiquido)
                .NotEmpty()
                .WithMessage("{PropertyName} é obrigatório e deve ser maior que zero.");

            RuleFor(x => x.QuantidadeDeParcelas)
                .NotEmpty()
                .WithMessage("{PropertyName} é obrigatória e deve ser maior que zero.");
        }
    }
}