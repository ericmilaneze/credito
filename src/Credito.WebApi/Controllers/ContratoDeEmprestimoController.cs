using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Application.ContratoDeEmprestimo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers
{
    [ApiController]
    [Route("api/contratos")]
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
                                                            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _mediator.Send(new ObterContratoPorIdQuery { Id = id },
                                                cancellationToken);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObterContratos([FromQuery] ObterContratosQuery query,
                                                        CancellationToken cancellationToken = default(CancellationToken)) =>
            Ok(await _mediator.Send(query, cancellationToken));

        [HttpPost]
        public async Task<IActionResult> CriarContrato(CriarContratoCmd cmd,
                                                       CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.Send(cmd, cancellationToken);
            return CreatedAtRoute(nameof(ObterContratoPorId),
                                  new { id = cmd.Id },
                                  null);
        }

        [HttpPost("calculo")]
        public async Task<IActionResult> CalcularContrato(CalcularContratoCmd cmd,
                                                          CancellationToken cancellationToken = default(CancellationToken)) =>
            Ok(await _mediator.Send(cmd, cancellationToken));
    }
}