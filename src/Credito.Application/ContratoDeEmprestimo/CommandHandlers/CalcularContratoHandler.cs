using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Application.Models;
using Credito.Domain.ContratoDeEmprestimo;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.CommandHandlers
{
    public class CalcularContratoHandler : IRequestHandler<CalcularContratoCmd, ContratoDeEmprestimoModel>
    {
        private readonly IMapper _mapper;

        public CalcularContratoHandler(IMapper mapper) =>
            _mapper = mapper;

        public async Task<ContratoDeEmprestimoModel> Handle(CalcularContratoCmd request, CancellationToken cancellationToken = default) =>
            await Task.FromResult(_mapper.Map<ContratoDeEmprestimoModel>(
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
                    })));
    }
}