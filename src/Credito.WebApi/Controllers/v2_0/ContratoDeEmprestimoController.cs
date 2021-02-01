using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Credito.Application.v2_0.ContratoDeEmprestimo.Commands;
using Credito.WebApi.Misc;
using Credito.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers.v2_0
{
    [ApiController]
    [Route(Globals.ROUTE_API_CONTRATOS)]
    [ApiVersion("2.0")]
    public class ContratoDeEmprestimoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContratoDeEmprestimoController(IMediator mediator) =>
            _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(ResourceErrorModel), (int)HttpStatusCode.Conflict)]
        public async Task<CreatedAtRouteResult> CriarContrato(CriarContratoCmd cmd,
                                                              CancellationToken cancellationToken = default)
        {
            await _mediator.Send(cmd, cancellationToken);
            return CreatedAtRoute(Globals.ROUTE_API_CONTRATOS_CRIAR_ROUTE_NAME,
                                  new { cmd.Id },
                                  null);
        }
    }
}