using System.Threading;
using System.Threading.Tasks;
using Credito.Application.Common.Exceptions;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Domain.ContratoDeEmprestimo;
using MediatR;
using Serilog;

namespace Credito.Application.ContratoDeEmprestimo.CommandHandlers
{
    public class CriarContratoHandler : AsyncRequestHandler<CriarContratoCmd>
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(CriarContratoHandler));

        private readonly IContratoDeEmprestimoRepository _repository;

        public CriarContratoHandler(IContratoDeEmprestimoRepository repository)
        {
            _repository = repository;
        }

        protected override async Task Handle(CriarContratoCmd request, CancellationToken cancellationToken)
        {
            _logger.Information("Criando contrato com os parâmetros {@ParametrosContrato}", request);
            await CheckIfAlreadyExists(request);
            await _repository.SaveAsync(
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
                    }),
                cancellationToken);
        }

        private async Task CheckIfAlreadyExists(CriarContratoCmd request)
        {
            var resource = await _repository.LoadAsync(request.Id);
            if (resource != null)
                throw new ResourceAlreadyExistsException(request);
        }
    }
}