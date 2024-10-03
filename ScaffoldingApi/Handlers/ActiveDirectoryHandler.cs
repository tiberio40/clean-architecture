
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace ANTpApi.Handlers
{
    public static class ActiveDirectoryHandler
    {
        public static void ConfigureActiveDirectory(IServiceCollection services, IConfigurationSection appSetting)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(appSetting);
        }
    }
}
