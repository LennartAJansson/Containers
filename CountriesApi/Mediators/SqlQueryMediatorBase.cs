namespace CountriesApi.Mediators
{
    using Countries.Model;

    public class SqlQueryMediatorBase
    {
        protected readonly ConnectionStrings connectionStrings;

        public SqlQueryMediatorBase(ConnectionStrings connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }
    }
}
