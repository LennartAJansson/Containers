namespace Workloads.Contract;

using MediatR;

public record QueryPeople() : IRequest<IEnumerable<QueryPersonResponse>>;
public record QueryPerson(Guid PersonId) : IRequest<QueryPersonResponse>;
public record QueryPersonResponse(Guid PersonId, string Name, IEnumerable<QueryWorkloadResponse> Workloads);

public record QueryAssignments() : IRequest<IEnumerable<QueryAssignmentResponse>>;
public record QueryAssignment(Guid AssignmentId) : IRequest<QueryAssignmentResponse>;
public record QueryAssignmentResponse(Guid AssignmentId, string CustomerName, string Description, IEnumerable<QueryWorkloadResponse> Workloads);

public record QueryWorkloads() : IRequest<IEnumerable<QueryWorkloadResponse>>;
public record QueryWorkload(Guid WorkloadId) : IRequest<QueryWorkloadResponse>;
public record QueryWorkloadResponse(Guid WorkloadId, DateTimeOffset Start, DateTimeOffset? Stop, QueryPersonResponse Person, QueryAssignmentResponse Assignment);
