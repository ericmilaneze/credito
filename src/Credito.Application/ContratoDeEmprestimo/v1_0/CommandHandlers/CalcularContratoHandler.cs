using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.v1_0.ContratoDeEmprestimo.Commands;
using Credito.Application.v1_0.Models;
using Credito.Domain.ContratoDeEmprestimo;
using MediatR;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.CommandHandlers
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
                        Id = Guid.NewGuid(),
                        ValorLiquido = request.ValorLiquido,
                        QuantidadeDeParcelas = request.QuantidadeDeParcelas,
                        TaxaAoMes = request.TaxaAoMes,
                        Tac = request.Tac,
                        Iof = request.Iof,
                        DiasDeCarencia = request.DiasDeCarencia,
                    })));
    }
}