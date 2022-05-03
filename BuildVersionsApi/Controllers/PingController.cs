// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildVersionsApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public string Ping() => "pong!";
    }
}
