using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Application.ContratoDeEmprestimo.Queries;
using Credito.Application.Models;
using Credito.WebApi.Misc;
using Credito.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers
{
    [ApiController]
    [Route(Globals.ROUTE_API_CONTRATOS)]
    [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DefaultErrorModel), (int)HttpStatusCode.InternalServerError)]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResourceErrorModel), (int)HttpStatusCode.NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ContratoDeEmprestimoModel>> ObterContratoPorId(Guid id,
                                                                                      CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(new ObterContratoPorIdQuery { Id = id },
                                    cancellationToken));

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<ContratoDeEmprestimoModel>>> ObterContratos([FromQuery] ObterContratosQuery query,
                                                                                               CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(query, cancellationToken));

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ResourceErrorModel), (int)HttpStatusCode.Conflict)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CriarContrato(CriarContratoCmd cmd,
                                                       CancellationToken cancellationToken = default)
        {
            await _mediator.Send(cmd, cancellationToken);
            return CreatedAtRoute(nameof(ObterContratoPorId),
                                  new { cmd.Id },
                                  null);
        }

        [HttpPost(Globals.ROUTE_API_CONTRATOS_CALCULO)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ContratoDeEmprestimoModel>> CalcularContrato(CalcularContratoCmd cmd,
                                                                                    CancellationToken cancellationToken = default) =>
            Ok(await _mediator.Send(cmd, cancellationToken));
    }
}