using Credito.Application.ContratoDeEmprestimo.Commands;
using FluentValidation;

namespace Credito.Application.ContratoDeEmprestimo.Validators
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

    public class CriarContratoV2Validator : AbstractValidator<CriarContratoCmdV2>
    {
        public CriarContratoV2Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} é obrigatório.");

            RuleFor(x => x.ClienteId)
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