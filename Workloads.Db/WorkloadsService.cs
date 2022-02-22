namespace Workloads.Db
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Workloads.Model;

    public interface IWorkloadsService
    {
        Task<Assignment> CreateAssignmentAsync(Assignment assignment);
        Task<Person> CreatePersonAsync(Person person);
        Task<Workload> CreateWorkloadAsync(Workload workload);
    }
    public class WorkloadsService : IWorkloadsService
    {
        private readonly ILogger<WorkloadsService> logger;
        private readonly IWorkloadsDbContext context;

        public WorkloadsService(ILogger<WorkloadsService> logger, IWorkloadsDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<Assignment> CreateAssignmentAsync(Assignment assignment)
        {
            context.Assignments.Add(assignment);
            await context.SaveChangesAsync();
            return assignment;
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            context.People.Add(person);
            await context.SaveChangesAsync();
            return person;
        }

        public async Task<Workload> CreateWorkloadAsync(Workload workload)
        {
            IEnumerable<Assignment> assignments = context.Assignments.Include("Workloads").Where(a => a.AssignmentId == Guid.Empty).ToList();
            IEnumerable<Person> people = context.People.Include("Workloads").Where(p => p.PersonId == Guid.Empty).ToList();
            IEnumerable<Workload> workloads = context.Workloads.Where(w => w.WorkloadId == Guid.Empty).Include("Assignment").Include("Person").ToList();
            context.Workloads.Add(workload);
            await context.SaveChangesAsync();
            return workload;
        }
    }
}
