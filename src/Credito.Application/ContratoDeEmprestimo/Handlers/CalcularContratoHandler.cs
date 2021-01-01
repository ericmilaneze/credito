using System.Threading;
using System.Threading.Tasks;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Domain.ContratoDeEmprestimo;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Handlers
{
    public class CalcularContratoHandler : IRequestHandler<CalcularContratoCmd, ContratoDeEmprestimoAggregate>
    {
        public async Task<ContratoDeEmprestimoAggregate> Handle(CalcularContratoCmd request, CancellationToken cancellationToken) =>
            await Task.FromResult(
                ContratoDeEmprestimoAggregate.CriarContrato(
                    new ContratoDeEmprestimoAggregate.ParametrosDeContratoDeEmprestimo
                    {
                        Id = request.Id,
                        ValorLiquido = request.ValorLiquido,
                        QuantidadeDeParcelas = request.QuantidadeDeParcelas,
                        TaxaAoMes = request.TaxaAoMes,
                        Tac = request.Tac,
                        Iof = request.Iof,
                        DiasDeCarencia = request.DiasDeCarencia,
                    }
                )
            );
    }
}