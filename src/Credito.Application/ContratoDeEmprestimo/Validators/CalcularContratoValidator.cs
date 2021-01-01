using Credito.Application.ContratoDeEmprestimo.Commands;
using FluentValidation;

namespace Credito.Application.ContratoDeEmprestimo.Validators
{
    public class CalcularContratoValidator : AbstractValidator<CalcularContratoCmd>
    {
        public CalcularContratoValidator()
        {
            RuleFor(x => x.ValorLiquido)
                .NotEmpty()
                .WithMessage("Valor líquido é obrigatório e deve ser maior que zero.");

            RuleFor(x => x.QuantidadeDeParcelas)
                .NotEmpty()
                .WithMessage("Quantidade de parcelas é obrigatória e deve ser maior que zero.");
        }
    }
}