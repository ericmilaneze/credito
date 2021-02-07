using Microsoft.AspNetCore.Mvc;

namespace Credito.WebApi.Controllers
{
    [ApiController]
    [Route("/api/credito/_monitor")]
    public class Monitor : ControllerBase
    {
        [HttpGet("shallow")]
        public OkObjectResult Shallow() =>
            Ok("It works!");
    }
}