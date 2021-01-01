using System.Threading;
using System.Threading.Tasks;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Domain.ContratoDeEmprestimo;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers
{
    [Route("api/contrato")]
    public class ContratoDeEmprestimoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContratoDeEmprestimoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarContrato([FromBody] CriarContratoCmd cmd,
                                                       CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _mediator.Send(cmd, cancellationToken);
                return Ok();
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("calculo")]
        public async Task<IActionResult> CalcularContrato([FromBody] CalcularContratoCmd cmd,
                                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var contrato = await _mediator.Send(cmd, cancellationToken);
                return Ok(contrato);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}