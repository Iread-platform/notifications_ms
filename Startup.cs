
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using AutoMapper;
using Consul;


using iread_notifications_ms.Web.Service;
using iread_notifications_ms.Web.Profile;
using iread_notifications_ms.Web.Utils;
using iread_notifications_ms.DataAccess;
using iread_notifications_ms.DataAccess.Repository;

namespace iread_notifications_ms
{
    public class Startup
    {
        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
           new LoggerFactory(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
           });


        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
             .AddJsonFile(Directory.GetCurrentDirectory() + "/Properties/launchSettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile(Directory.GetCurrentDirectory() + "/appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
            .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.AddDbContext<AppDbContext>(
           options =>
           {
               options.UseLoggerFactory(_myLoggerFactory).UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
           });
            //         services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            //    {
            //        var address = Configuration.GetValue<string>("ConsulConfig:Host");
            //        consulConfig.Address = new System.Uri(address);
            //    }
            //    ));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iread_notifications_ms", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                           "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            // Inject repos and services.
            services.AddScoped<IPublicRepo, PublicRepo>();
            services.AddSingleton<IFirebaseMessagingService, FirebaseMessagingService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<UserService>();
            services.AddScoped<TopicService>();
            IMapper mapper = new MapperConfiguration(config =>
            {
                config.AddProfile<AutoMapperProfile>();
            }).CreateMapper();
            services.AddSingleton(mapper);
            // services.AddHttpClient<IConsulHttpClientService, ConsulHttpClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iread_notifications_ms v1"));
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }


            app.UseRouting();
            app.UseCors("_myAllowSpecificOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // app.UseConsul(Configuration);

        }
    }
}
