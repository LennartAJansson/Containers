namespace Workloads.Contract
{
    using MediatR;

    public record CommandCreatePerson(string Name) : IRequest<CommandPersonResponse>;
    public record CommandUpdatePerson(int PersonId, string Name) : IRequest<CommandPersonResponse>;
    public record CommandDeletePerson(int PersonId) : IRequest<CommandPersonResponse>;

    public record CommandPersonResponse(int PersonId, string Status);

    public record CommandCreateAssignment(string Customer, string Description) : IRequest<CommandAssignmentResponse>;
    public record CommandUpdateAssignment(int AssignmentId, string Customer, string Description) : IRequest<CommandAssignmentResponse>;
    public record CommandDeleteAssignment(int AssignmentId) : IRequest<CommandAssignmentResponse>;

    public record CommandAssignmentResponse(int AssignmentId, string Status);

    public record CommandCreateWorkload(DateTimeOffset Start, int PersonId, int AssignmentId) : IRequest<CommandWorkloadResponse>;
    public record CommandUpdateWorkload(int WorkloadId) : IRequest<CommandWorkloadResponse>;
    public record CommandDeleteWorkload(int WorkloadId) : IRequest<CommandWorkloadResponse>;

    public record CommandWorkloadResponse(int WorkloadId, string Status);
}
