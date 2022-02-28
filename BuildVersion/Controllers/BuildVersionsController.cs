// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildVersion.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildVersionsController : ControllerBase
    {
        private readonly ILogger<BuildVersionsController> logger;
        private readonly BuildVersionsDb context;

        public BuildVersionsController(ILogger<BuildVersionsController> logger, BuildVersionsDb context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get() => Ok(context.BuildVersions.ToList());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(context.BuildVersions.SingleOrDefault(bv => bv.Id == id));

        [HttpPost]
        public IActionResult Post([FromBody] BuildVersion buildVersion)
        {
            logger.LogDebug("");
            context.BuildVersions.Add(buildVersion);
            context.SaveChanges();
            return Ok(buildVersion);
        }

        [HttpPut]
        public IActionResult Put([FromBody] BuildVersion buildVersion)
        {
            context.BuildVersions.Update(buildVersion);
            context.SaveChanges();
            return Ok(buildVersion);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            BuildVersion? buildVersion = context.BuildVersions.Find(id);
            if (buildVersion != null)
            {
                context.BuildVersions.Remove(buildVersion);
                context.SaveChanges();
                return Ok(buildVersion);
            }
            return BadRequest();
        }
    }
}
