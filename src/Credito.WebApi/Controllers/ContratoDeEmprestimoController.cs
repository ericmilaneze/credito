using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.WebApi.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers
{
    [Route("api/contrato")]
    public class ContratoDeEmprestimoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ContratoDeEmprestimoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CriarContrato([FromBody] CriarContratoCmd cmd,
                                                       CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.Send(cmd, cancellationToken);
            return Ok();
        }

        [HttpPost("calculo")]
        public async Task<IActionResult> CalcularContrato([FromBody] CalcularContratoCmd cmd,
                                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            var contratoCalculado = await _mediator.Send(cmd, cancellationToken);
            return Ok(_mapper.Map<ContratoDeEmprestimoModel>(contratoCalculado));
        }
    }
}