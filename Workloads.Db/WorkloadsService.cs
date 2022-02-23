namespace Workloads.Db
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Workloads.Model;

    public interface IWorkloadsService
    {
        Task<Assignment> CreateAssignmentAsync(Assignment assignment);
        Task<Person> CreatePersonAsync(Person person);
        Task<Workload> CreateWorkloadAsync(Workload workload);

        Task<Assignment> UpdateAssignmentAsync(Assignment assignment);
        Task<Person> UpdatePersonAsync(Person person);
        Task<Workload> UpdateWorkloadAsync(Workload workload);

        Task<Assignment> DeleteAssignmentAsync(Guid assignmentId);
        Task<Person> DeletePersonAsync(Guid personId);
        Task<Workload> DeleteWorkloadAsync(Guid workloadId);
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
            context.Workloads.Add(workload);
            await context.SaveChangesAsync();
            return workload;
        }

        public async Task<Assignment> UpdateAssignmentAsync(Assignment assignment)
        {
            context.Assignments.Update(assignment);
            await context.SaveChangesAsync();
            return assignment;
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            context.People.Update(person);
            await context.SaveChangesAsync();
            return person;
        }

        public async Task<Workload> UpdateWorkloadAsync(Workload workload)
        {
            context.Workloads.Update(workload);
            await context.SaveChangesAsync();
            return workload;
        }

        public async Task<Assignment> DeleteAssignmentAsync(Guid assignmentId)
        {
            Assignment? assignment = await context.Assignments.FindAsync(assignmentId);
            context.Assignments.Remove(assignment);
            await context.SaveChangesAsync();
            return assignment;
        }

        public async Task<Person> DeletePersonAsync(Guid personId)
        {
            Person? person = await context.People.FindAsync(personId);
            context.People.Remove(person);
            await context.SaveChangesAsync();
            return person;
        }

        public async Task<Workload> DeleteWorkloadAsync(Guid workloadId)
        {
            Workload? workload = await context.Workloads.FindAsync(workloadId);
            context.Workloads.Remove(workload);
            await context.SaveChangesAsync();
            return workload;
        }

    }
}
