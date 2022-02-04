namespace WorkloadsApi.Mediators
{
    using System.Reflection;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediators(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(MediatorExtensions)) ?? throw new NullReferenceException());

            return services;
        }
    }
}