namespace WorkloadsProjector.Mediators.Commands
{

    using Workloads.Db;

    public class ProjectorMediatorBase
    {
        protected readonly IWorkloadsService service;

        public ProjectorMediatorBase(IWorkloadsService service) => this.service = service;
    }
}
