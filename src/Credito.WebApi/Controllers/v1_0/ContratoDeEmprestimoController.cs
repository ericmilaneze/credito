using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Credito.Application.v1_0.ContratoDeEmprestimo.Commands;
using Credito.Application.v1_0.ContratoDeEmprestimo.Queries;
using Credito.Application.v1_0.Models;
using Credito.WebApi.Misc;
using Credito.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers.v1_0
{
    [ApiController]
    [Route(Globals.ROUTE_API_CONTRATOS)]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ContratoDeEmprestimoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContratoDeEmprestimoController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet("{id}", Name = Globals.ROUTE_API_CONTRATOS_CRIAR_ROUTE_NAME)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResourceErrorModel), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ContratoDeEmprestimoModel>> ObterContratoPorId(Guid id,
                                                                                      CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(new ObterContratoPorIdQuery { Id = id },
                                    cancellationToken));

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContratoDeEmprestimoModel>>> ObterContratos([FromQuery] ObterContratosQuery query,
                                                                                               CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(query, cancellationToken));

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ResourceErrorModel), (int)HttpStatusCode.Conflict)]
        public async Task<CreatedAtRouteResult> CriarContrato(CriarContratoCmd cmd,
                                                              CancellationToken cancellationToken = default)
        {
            await _mediator.Send(cmd, cancellationToken);
            return CreatedAtRoute(nameof(ObterContratoPorId),
                                  new { cmd.Id },
                                  null);
        }

        [HttpPost(Globals.ROUTE_API_CONTRATOS_CALCULO)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ContratoDeEmprestimoModel>> CalcularContrato(CalcularContratoCmd cmd,
                                                                                    CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(cmd, cancellationToken));
    }
}