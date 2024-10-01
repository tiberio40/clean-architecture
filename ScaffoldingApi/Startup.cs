using Application;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Hangfire.Storage;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization;
using ScaffoldingApi.Handlers;
using Infrastructure.Options;
using Application.Interfaces;
using Application.Tmetric;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Implementa aquí tu lógica de autorización
            // Por ejemplo, puedes verificar si el usuario está autenticado o si tiene un rol específico

            // Ejemplo básico: permitir el acceso sin autenticación (para pruebas)
            return true;
        }
    }


    public void ConfigureServices(IServiceCollection services)
    {
        #region Hangfire
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        // Agregar autenticación básica para el dashboard de Hangfire
        services.AddHangfireServer();

        services.AddSingleton<IRecurringJobService, RecurringJobService>();
        services.AddSingleton<IStaticRecurringJobService, StaticRecurringJobService>();
        services.AddScoped<JobMethodsService>();



        // Registrar IMonitoringApi si es necesario
        services.AddSingleton<IMonitoringApi>(provider =>
        {
            var storage = provider.GetRequiredService<JobStorage>();
            return storage.GetMonitoringApi();
        });




        #endregion

        #region MongoDB
        services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDB"));
        services.AddSingleton<MongoDbContext>();
        BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());

        #endregion

        #region SQL Server Connection
        services.AddDbContext<SqlDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
        });
        services.AddTransient<IMembersRequestService, MembersRequestService>();
        services.AddTransient<ITmetricRequestService, TmetricRequestService>();

        #endregion

        #region Inyection
        DependencyInyectionHandler.DependencyInyectionConfig(services, Configuration);
        #endregion

        #region JWT
        var tokenAppSetting = Configuration.GetSection("Tokens");
        JwtConfigurationHandler.ConfigureJwtAuthentication(services, tokenAppSetting);
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


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobService recurringJobService, IStaticRecurringJobService staticRecurringJobService)
    {

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Hangfire Dashboard",
            Authorization = new[] { new HangfireAuthorizationFilter() }
        });

        app.UseHangfireServer();
        if (Configuration["HangFire:Habilitado"] == "1")
        {
            // Programar los trabajos recurrentes dinámicos y estáticos
            recurringJobService.ScheduleRecurringJobs();
            staticRecurringJobService.ScheduleStaticRecurringJobs();
        }





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
    