namespace WorkloadsApi.Mediators.Queries
{
    using Workloads.Model;

    public class SqlQueryMediatorBase
    {
        protected readonly ConnectionStrings connectionStrings;

        public SqlQueryMediatorBase(ConnectionStrings connectionStrings) => this.connectionStrings = connectionStrings;
    }
}
