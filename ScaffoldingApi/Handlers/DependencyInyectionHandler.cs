using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWork.Interfaces;

using Microsoft.Extensions.Options;



namespace ANTpApi.Handlers
{
    public static class DependencyInyectionHandler
    {

        public static void DependencyInyectionConfig(IServiceCollection services, IConfiguration configuration)
        {

            // Infrastructure
            services.AddScoped<CustomValidationFilterAttribute, CustomValidationFilterAttribute>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<SeedDb>();     

        }
    }
}
