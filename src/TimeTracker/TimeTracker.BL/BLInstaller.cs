using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL
{
    public static class BLInstaller
    {
        public static IServiceCollection AddBLServices(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

            services.Scan(selector => selector
                .FromAssemblyOf<BusinessLogic>()
                .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
                .AsMatchingInterface()
                .WithSingletonLifetime());

            services.Scan(selector => selector
                .FromAssemblyOf<BusinessLogic>()
                .AddClasses(filter => filter.AssignableTo(typeof(ModelMapperBase<,,>)))
                .AsMatchingInterface()
                .WithSingletonLifetime());

            return services;
        }
    }
}
