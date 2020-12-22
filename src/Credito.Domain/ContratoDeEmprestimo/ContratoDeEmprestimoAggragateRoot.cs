using Credito.Domain.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo
{
    public class ContratoDeEmprestimoAggragateRoot
    {
        public ValorMonetarioPositivo ValorLiquido { get; }

        public ContratoDeEmprestimoAggragateRoot(ParametrosDeContratoDeEmprestimo parametros)
        {
            ValorLiquido = parametros.ValorLiquido;
        }

        public record ParametrosDeContratoDeEmprestimo
        {
            public ValorMonetarioPositivo ValorLiquido { get; init; }
            //public Prazo MyProperty { get; init; }
        }
    }
}