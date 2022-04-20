// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildVersion.Controllers
{
    using BuildVersion.Data;

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
        public IActionResult Get()
        {
            logger.LogDebug("Get all BuildVersion");

            List<BuildVersion> result = context.BuildVersions.ToList();

            return (result != null && result.Count != 0) ? Ok(result) : BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            logger.LogDebug("Get BuildVersion by id");

            BuildVersion result = context.BuildVersions.SingleOrDefault(bv => bv.Id == id);

            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody] BuildVersion buildVersion)
        {
            logger.LogDebug("Post BuildVersion");

            try
            {
                context.BuildVersions.Add(buildVersion);
                context.SaveChanges();
                return Ok(buildVersion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] BuildVersion buildVersion)
        {
            logger.LogDebug("Put BuildVersion");

            try
            {
                context.BuildVersions.Update(buildVersion);
                context.SaveChanges();
                return Ok(buildVersion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            logger.LogDebug("Delete BuildVersion by id");

            BuildVersion buildVersion = context.BuildVersions.Find(id);
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
