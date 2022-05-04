// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildVersionsApi.Controllers
{

    using BuildVersionsApi.Data;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BuildVersion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult GetBuildVersions()
        {
            logger.LogDebug("Get all BuildVersion");

            IEnumerable<BuildVersion> result = context.BuildVersions!.Include(x => x.Binary);

            return result != null && result.Any() ? Ok(result) : NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildVersion))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public IActionResult GetBuildVersionById(int id)
        {
            logger.LogDebug("Get BuildVersion by id");

            BuildVersion? result = context.BuildVersions!.Include(x => x.Binary).SingleOrDefault(bv => bv.Id == id);

            return result != null ? Ok(result) : NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildVersion))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public IActionResult PostBuildVersion([FromBody] BuildVersion buildVersion)
        {
            logger.LogDebug("Post BuildVersion");

            try
            {
                _ = context.BuildVersions!.Add(buildVersion);
                _ = context.SaveChanges();
                return Ok(buildVersion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildVersion))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public IActionResult PutBuildVersion([FromBody] BuildVersion buildVersion)
        {
            logger.LogDebug("Put BuildVersion");

            try
            {
                _ = context.BuildVersions!.Update(buildVersion);
                _ = context.SaveChanges();
                return Ok(buildVersion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildVersion))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult DeleteBuildVersion(int id)
        {
            logger.LogDebug("Delete BuildVersion by id");

            BuildVersion? buildVersion = context.BuildVersions!.Include(x => x.Binary).SingleOrDefault(bv => bv.Id == id);
            if (buildVersion != null)
            {
                _ = context.BuildVersions!.Remove(buildVersion);
                _ = context.SaveChanges();
                return Ok(buildVersion);
            }

            return NotFound();
        }
    }
}
