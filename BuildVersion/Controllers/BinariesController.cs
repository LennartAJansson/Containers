// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildVersion.Controllers
{
    using BuildVersion.Data;

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

        [HttpGet]
        public IActionResult Get()
        {
            logger.LogDebug("Get all Binaries");

            List<Binary> result = context.Binaries.Include("BuildVersion").ToList();

            return (result != null && result.Count != 0) ? Ok(result) : BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            logger.LogDebug("Get Binary by id");

            Binary result = context.Binaries.Include("BuildVersion").SingleOrDefault(b => b.Id == id);

            return result != null ? Ok(result) : BadRequest();
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            logger.LogDebug("Get Binary by name");

            Binary result = context.Binaries.Include("BuildVersion").SingleOrDefault(b => b.ProjectFile.ToLower() == name.ToLower());

            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Binary binary)
        {
            logger.LogDebug("Post Binary");

            try
            {
                context.Binaries.Add(binary);
                context.SaveChanges();
                return Ok(binary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult PutById([FromBody] Binary binary)
        {
            logger.LogDebug("Put Binary by id");

            try
            {
                context.Binaries.Update(binary);
                context.SaveChanges();
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            logger.LogDebug("Delete Binary by id");

            Binary binary = context.Binaries.Find(id);
            if (binary != null)
            {
                context.Binaries.Remove(binary);
                context.SaveChanges();
                return Ok(binary);
            }

            return BadRequest();
        }

        [HttpGet("{name}")]
        public IActionResult MajorInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Major");

            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Major++;
                    context.SaveChanges();
                    return Ok(binary);
                }
            }

            return BadRequest();
        }

        [HttpGet("{name}")]
        public IActionResult MinorInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Minor");

            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Minor++;
                    context.SaveChanges();
                    return Ok(binary);
                }
            }

            return BadRequest();
        }

        [HttpGet("{name}")]
        public IActionResult BuildInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Build");

            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Build++;
                    context.SaveChanges();
                    return Ok(binary);
                }
            }

            return BadRequest();
        }

        [HttpGet("{name}")]
        public IActionResult RevisionInc(string name)
        {
            logger.LogDebug("Get Binary by name and increase Revision");

            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.Revision++;
                    context.SaveChanges();
                    return Ok(binary);
                }
            }

            return BadRequest();
        }

        [HttpPost("{name}")]
        public IActionResult SetSemVerPre(string name, string semverpre)
        {
            logger.LogDebug("Get Binary by name and set SemanticVersionPre");

            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
                if (buildVersion != null)
                {
                    buildVersion.SemanticVersionPre = semverpre;
                    context.SaveChanges();
                    return Ok(binary);
                }
            }

            return BadRequest();
        }

        private Binary GetOrCreate(string name)
        {
            logger.LogDebug("Trying to get {name}", name);

            Binary binary = context.Binaries
                .Include("BuildVersion")
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
                context.Binaries.Add(binary);
                int result = context.SaveChanges();
                logger.LogDebug("Creating buildversion registration for {name} with id {id}", binary.ProjectFile, binary.Id);
            }

            return binary;
        }
    }
}

