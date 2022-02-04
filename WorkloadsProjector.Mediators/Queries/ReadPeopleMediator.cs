namespace WorkloadsProjector.Mediators.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class ReadPeopleMediator : IRequestHandler<QueryPeople, IEnumerable<QueryPersonResponse>>
    {
        private readonly ILogger<ReadPeopleMediator> logger;

        public ReadPeopleMediator(ILogger<ReadPeopleMediator> logger) => this.logger = logger;

        public Task<IEnumerable<QueryPersonResponse>> Handle(QueryPeople request, CancellationToken cancellationToken) =>
            throw new NotImplementedException();
    }

}