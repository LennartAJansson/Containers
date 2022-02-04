namespace Workloads.Contract
{
    using MediatR;

    public record CommandCreatePerson(string Name) : IRequest<CommandPersonResponse>;
    public record CommandUpdatePerson(int PersonId) : IRequest<CommandPersonResponse>;
    public record CommandDeletePerson(int PersonId) : IRequest<CommandPersonResponse>;

    public record CommandPersonResponse(int PersonId, string Status);

}
