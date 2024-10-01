﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ScaffoldingApi.Handlers
{
    public class JwtConfigurationHandler
    {
        public static void ConfigureJwtAuthentication(IServiceCollection services, IConfigurationSection tokenAppSetting)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddCookie()
               .AddJwtBearer(cfg =>
               {
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = tokenAppSetting.GetSection("Issuer").Value,
                       ValidAudience = tokenAppSetting.GetSection("Audience").Value,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenAppSetting.GetSection("Key").Value))
                   };
               });
        }
    }
}
