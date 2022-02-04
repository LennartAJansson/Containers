namespace Workloads.Contract
{
    using MediatR;

    public record QueryPeople() : IRequest<IEnumerable<QueryPersonResponse>>;
    public record QueryPerson(int PersonId) : IRequest<QueryPersonResponse>;
    public record QueryPersonResponse();

}