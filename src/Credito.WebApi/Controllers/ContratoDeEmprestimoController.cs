using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Application.ContratoDeEmprestimo.Queries;
using Credito.WebApi.Misc;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers
{
    [ApiController]
    [Route(Globals.ROUTE_API_CONTRATOS)]
    public class ContratoDeEmprestimoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ContratoDeEmprestimoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(ObterContratoPorId))]
        public async Task<IActionResult> ObterContratoPorId(Guid id,
                                                            CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(new ObterContratoPorIdQuery { Id = id },
                                    cancellationToken));

        [HttpGet]
        public async Task<IActionResult> ObterContratos([FromQuery] ObterContratosQuery query,
                                                        CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(query, cancellationToken));

        [HttpPost]
        public async Task<IActionResult> CriarContrato(CriarContratoCmd cmd,
                                                       CancellationToken cancellationToken = default)
        {
            await _mediator.Send(cmd, cancellationToken);
            return CreatedAtRoute(nameof(ObterContratoPorId),
                                  new { cmd.Id },
                                  null);
        }

        [HttpPost(Globals.ROUTE_API_CONTRATOS_CALCULO)]
        public async Task<IActionResult> CalcularContrato(CalcularContratoCmd cmd,
                                                          CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(cmd, cancellationToken));
    }
}