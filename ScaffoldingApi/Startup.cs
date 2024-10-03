using Infrastructure.Configurations;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ANTpApi.Handlers;



public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    


    public void ConfigureServices(IServiceCollection services)
    {
        

        #region SQL Server Connection
        services.AddDbContext<SqlDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
        });

        #endregion

        #region Inyection
        DependencyInyectionHandler.DependencyInyectionConfig(services, Configuration);
        #endregion


        #region CustimValidationFilterAttribute
        services.Configure<ApiBehaviorOptions>(options
        => options.SuppressModelStateInvalidFilter = true);
        #endregion

        services.AddCors(Options =>
        {
            Options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .WithExposedHeaders("Location");

            });
        });

        services.AddControllers();
        // Otros servicios necesarios

        //SWAGGER
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Scaffolding API",
                Version = "v1",
                Description = "Api base",
                Contact = new OpenApiContact
                {
                    Name = "Tu nombre",
                    Email = "tu@email.com",
                    Url = new Uri("https://tu-sitio-web.com"),
                }
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {      

        // Configurar Hangfire Server
        // Enable middleware to serve generated Swagger as a JSON endpoint
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scaffolding API");
            // Configura la ruta de la interfaz de usuario de Swagger
            c.RoutePrefix = "swagger";
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


    }
}
    