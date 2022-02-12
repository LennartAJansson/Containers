namespace Workloads.Contract;

using MediatR;

public record CommandCreatePerson(string Name) : IRequest<CommandPersonResponse>;
public record CommandCreatePersonWithId(Guid PersonId, string Name) : CommandCreatePerson(Name), IRequest<CommandPersonResponse>;
public record CommandUpdatePerson(Guid PersonId, string Name) : IRequest<CommandPersonResponse>;
public record CommandDeletePerson(Guid PersonId) : IRequest<CommandPersonResponse>;

public record CommandPersonResponse(Guid PersonId, string Status);//Return Guid.Empty if failed and Information in Status

public record CommandCreateAssignment(string CustomerName, string Description) : IRequest<CommandAssignmentResponse>;
public record CommandCreateAssignmentWithId(Guid AssignmentId, string CustomerName, string Description) : IRequest<CommandAssignmentResponse>;
public record CommandUpdateAssignment(Guid AssignmentId, string Customer, string Description) : IRequest<CommandAssignmentResponse>;
public record CommandDeleteAssignment(Guid AssignmentId) : IRequest<CommandAssignmentResponse>;

public record CommandAssignmentResponse(Guid AssignmentId, string Status);//Return Guid.Empty if failed and Information in Status

public record CommandCreateWorkload(DateTimeOffset Start, DateTimeOffset? Stop, Guid PersonId, Guid AssignmentId) : IRequest<CommandWorkloadResponse>;
public record CommandCreateWorkloadWithId(Guid WorkloadId, DateTimeOffset Start, DateTimeOffset? Stop, Guid PersonId, Guid AssignmentId) : IRequest<CommandWorkloadResponse>;
public record CommandUpdateWorkload(Guid WorkloadId, DateTimeOffset Start, DateTimeOffset? Stop, Guid PersonId, Guid AssignmentId) : IRequest<CommandWorkloadResponse>;
public record CommandDeleteWorkload(Guid WorkloadId) : IRequest<CommandWorkloadResponse>;

public record CommandWorkloadResponse(Guid WorkloadId, string Status);//Return Guid.Empty if failed and Information in Status
