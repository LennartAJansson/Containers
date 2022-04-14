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
        public IActionResult Get() => Ok(context.Binaries.Include("BuildVersion").ToList());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(context.Binaries.Include("BuildVersion").SingleOrDefault(b => b.Id == id));
        [HttpGet("{name}")]
        public IActionResult GetByName(string name) => Ok(context.Binaries.Include("BuildVersion").SingleOrDefault(b => b.ProjectFile == name));

        [HttpPost]
        public IActionResult Post([FromBody] Binary binary)
        {
            logger.LogDebug("");
            context.Binaries.Add(binary);
            context.SaveChanges();
            return Ok(binary);
        }

        [HttpPut]
        public IActionResult PutById([FromBody] Binary binary)
        {
            context.Binaries.Update(binary);
            context.SaveChanges();
            return Ok(binary);
        }

        [HttpPut]
        public IActionResult PutByName([FromBody] Binary binary)
        {
            context.Binaries.Update(binary);
            context.SaveChanges();
            return Ok(binary);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Binary? binary = context.Binaries.Find(id);
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
            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
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
            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
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
            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
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
            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
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
            Binary binary = GetOrCreate(name);

            if (binary != null)
            {
                BuildVersion? buildVersion = context.BuildVersions.SingleOrDefault(bv => bv.Id == binary.BuildVersionId);
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
            //TODO Doesn't work initially when name is not found
            logger.LogInformation("Trying to get {name}", name);

            Binary? binary = context.Binaries.Include("BuildVersion").SingleOrDefault(b => b.ProjectFile == name);

            if (binary == null)
            {
                BuildVersion? buildVersion = new BuildVersion { Major = 0, Minor = 1, Build = 0, Revision = 0, SemanticVersionPre = "dev" };
                binary = new Binary
                {
                    ProjectFile = name,
                    BuildVersion = buildVersion
                };
                context.Binaries.Add(binary);
                int result = context.SaveChanges();
                logger.LogInformation("Creating buildversion registration for {name} with id {id}", binary.ProjectFile, binary.Id);
            }

            return binary;
        }
    }
}

