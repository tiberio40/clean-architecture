using Application.Interfaces;
using Application.Interfaces.Scaffolding;
using Application.Interfaces.Security;
using Application.Services;
using Application.Services.Scaffolding;
using Application.Services.Security;
using Application.Tmetric;
using Core.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWork.Interfaces;

using Infrastructure.Repositories.Interfaces;
using Application.Common;
using Application.Services.Campaign;
using Application.Interfaces.Campaign;
using Infrastructure.Options;
using Core.DTOs.Campaign.Marketing;
using Microsoft.Extensions.Options;



namespace ScaffoldingApi.Handlers
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

            //Domain
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRolServices, RolServices>();
            services.AddScoped<IPermissionServices, PermissionServices>();
            services.AddScoped<IMenuServices, MenuServices>();
            services.AddScoped<IExternalRequestClient, ExternalRequestClient>();
            services.AddScoped<ITmetricRequestService, TmetricRequestService>();
            services.AddScoped<IMembersRequestService, MembersRequestService>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<INormalizeProjectNamesServices, NormalizeProjectNamesServices>();
            services.AddScoped<IConfigThemeServices, ConfigThemeServices>();
            services.AddSingleton<AuthorizationServiceFactory>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IMetaTypeService, MetaTypeService>();
            services.AddScoped<IMarketingService, MarketingService>();

            services.AddScoped<IAuthConfigRepository, AuthConfigRepository>();
            services.AddScoped<IAuthConfigService, AuthConfigService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
            services.AddScoped<IMembersRepository, MembersRepository>();


            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<WhatsAppConfig>(configuration.GetRequiredSection("WhatsApp"));
            services.AddSingleton(cfg => cfg.GetRequiredService<IOptions<WhatsAppConfig>>().Value);

        }
    }
}
