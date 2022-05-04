// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildVersionsApi.Controllers
{

    using BuildVersionsApi.Data;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BinariesController : ControllerBase
    {
        private readonly ILogger<BinariesController> logger;
        private readonly BuildVersionsDb context;

        public BinariesController(ILogger<BinariesController> logger, BuildVersionsDb context)
        {
            this.logger = logger;
            this.context = context;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Binary>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult GetBinaries()
        {
            logger.LogDebug("Get all Binaries");

            IEnumerable<Binary> result = context.Binaries!.Include("BuildVersion");

            return result != null && result.Any() ? Ok(result) : NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public IActionResult GetBinaryById(int id)
        {
            logger.LogDebug("Get Binary by id");

            Binary? result = context.Binaries!.Include("BuildVersion").SingleOrDefault(b => b.Id == id);

            return result != null ? Ok(result) : BadRequest();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public IActionResult GetBinaryByName(string name)
        {
            logger.LogDebug("Get Binary by name");

            Binary? result = context.Binaries!.Include("BuildVersion").SingleOrDefault(b => b.ProjectFile.ToLower() == name.ToLower());

            return result != null ? Ok(result) : NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public IActionResult PostBinary([FromBody] Binary binary)
        {
            logger.LogDebug("Post Binary");

            try
            {
                _ = context.Binaries!.Add(binary);
                _ = context.SaveChanges();
                return Ok(binary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public IActionResult PutBinaryById([FromBody] Binary binary)
        {
            logger.LogDebug("Put Binary by id");

            try
            {
                _ = context.Binaries!.Update(binary);
                _ = context.SaveChanges();
                return Ok(binary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut]
        //public IActionResult PutByName([FromBody] Binary binary)
        //{
        //    logger.LogDebug("Put Binary by id");

        //    try
        //    {
        //        context.Binaries.Update(binary);
        //        context.SaveChanges();
        //        return Ok(binary);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult DeleteBinary(int id)
        {
            logger.LogDebug("Delete Binary by id");

            Binary? binary = context.Binaries!.Include(f => f.BuildVersion).SingleOrDefault(b => b.Id == id);
            if (binary != null)
            {
                _ = context.Binaries!.Remove(binary);
                _ = context.SaveChanges();
                return Ok(binary);
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public IActionResult MajorInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Major");

            Binary? binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions!.Include(f => f.Binary).SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Major++;
                    _ = context.SaveChanges();
                    return Ok(binary);
                }
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public IActionResult MinorInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Minor");

            Binary? binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions!.Include(f => f.Binary).SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Minor++;
                    _ = context.SaveChanges();
                    return Ok(binary);
                }
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public IActionResult BuildInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Build");

            Binary? binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions!.Include(f => f.Binary).SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Build++;
                    _ = context.SaveChanges();
                    return Ok(binary);
                }
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public IActionResult RevisionInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Revision");

            Binary? binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions!.Include(f => f.Binary).SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Revision++;
                    _ = context.SaveChanges();
                    return Ok(binary);
                }
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Binary))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{name}")]
        public IActionResult SetSemVerPre(string name, string semverpre)
        {
            logger.LogDebug("Get Binary by name and set SemanticVersionPre");

            Binary? binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions!.Include(f => f.Binary).SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.SemanticVersionPre = semverpre;
                    _ = context.SaveChanges();
                    return Ok(binary);
                }
            }

            return NotFound();
        }

        private Binary GetOrCreate(string name)
        {
            logger.LogDebug("Trying to get {name}", name);

            Binary? binary = context.Binaries!
                .Include(f => f.BuildVersion)
                .SingleOrDefault(b => b.ProjectFile.ToLower() == name.ToLower());

            if (binary == null)
            {
                binary = new Binary
                {
                    ProjectFile = name.ToLower(),
                    BuildVersion = new BuildVersion
                    {
                        Major = 0,
                        Minor = 1,
                        Build = 0,
                        Revision = 0,
                        SemanticVersionPre = "dev"
                    }
                };
                _ = context.Binaries!.Add(binary);
                int result = context.SaveChanges();
                logger.LogDebug("Creating buildversion registration for {name} with id {id}", binary.ProjectFile, binary.Id);
            }

            return binary;
        }
    }
}

