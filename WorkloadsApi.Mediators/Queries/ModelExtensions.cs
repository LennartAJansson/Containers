namespace WorkloadsApi.Mediators.Queries
{
    using System.Linq;

    using Workloads.Contract;
    using Workloads.Model;

    public static class ModelExtensions
    {
        public static QueryPersonResponse ToUnJoinedQueryPersonResponse(this Person person) =>
            new QueryPersonResponse(person.PersonId, person!.Name!, Enumerable.Empty<QueryWorkloadResponse>());

        public static QueryAssignmentResponse ToUnJoinedQueryAssignmentResponse(this Assignment assignment) =>
            new QueryAssignmentResponse(assignment.AssignmentId, assignment!.CustomerName!, assignment!.Description!, Enumerable.Empty<QueryWorkloadResponse>());

        public static QueryWorkloadResponse ToUnJoinedQueryWorkloadResponse(this Workload workload) =>
            new QueryWorkloadResponse(workload.WorkloadId, workload.Start, workload.Stop, workload.Person!.ToUnJoinedQueryPersonResponse(), workload.Assignment!.ToUnJoinedQueryAssignmentResponse());
    }
}