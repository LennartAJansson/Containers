namespace Workloads.Contract
{
    using MediatR;

    public record QueryPeople() : IRequest<IEnumerable<QueryPersonResponse>>;
    public record QueryPerson(int PersonId) : IRequest<QueryPersonResponse>;
    public record QueryPersonResponse(int PersonId, string Name, IEnumerable<QueryWorkloadResponse> Workloads);

    public record QueryAssignments() : IRequest<IEnumerable<QueryAssignmentResponse>>;
    public record QueryAssignment(int AssignmentId) : IRequest<QueryAssignmentResponse>;
    public record QueryAssignmentResponse(int AssignmentId, string Customer, string Description);

    public record QueryWorkloads() : IRequest<IEnumerable<QueryAssignmentResponse>>;
    public record QueryWorkload(int AssignmentId) : IRequest<QueryAssignmentResponse>;
    public record QueryWorkloadResponse();
}