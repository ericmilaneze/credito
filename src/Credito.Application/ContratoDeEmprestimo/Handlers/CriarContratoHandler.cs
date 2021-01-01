using System.Threading;
using System.Threading.Tasks;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Domain.ContratoDeEmprestimo;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Handlers
{
    public class CriarContratoHandler : AsyncRequestHandler<CriarContratoCmd>
    {
        private readonly IContratoDeEmprestimoRepository _repository;

        public CriarContratoHandler(IContratoDeEmprestimoRepository repository)
        {
            _repository = repository;
        }

        protected override async Task Handle(CriarContratoCmd request, CancellationToken cancellationToken)
        {
            var contrato = ContratoDeEmprestimoAggregate.CriarContrato(
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
            );
            await _repository.SaveAsync(contrato, cancellationToken);
        }
    }
}