namespace WorkloadsApi.Mediators.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class ReadPersonMediator : IRequestHandler<QueryPerson, QueryPersonResponse>
    {
        private readonly ILogger<ReadPersonMediator> logger;

        public ReadPersonMediator(ILogger<ReadPersonMediator> logger) => this.logger = logger;

        public Task<QueryPersonResponse> Handle(QueryPerson request, CancellationToken cancellationToken) =>
            throw new NotImplementedException();
    }
}