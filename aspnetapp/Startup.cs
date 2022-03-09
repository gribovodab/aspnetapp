using System;
using System.IO;
using System.Reflection;
using aspnetapp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace aspnetapp
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        private IConfiguration configuration { get; }

        /// <summary>
        /// Constructor injection
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Метод для добавления служб в контейнер.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;

                    options.AssumeDefaultVersionWhenUnspecified = true;
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "",
                    Title = "",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "Hello world"
                    },
                   
                });

                c.DescribeAllParametersInCamelCase();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddGrpc();
            
            services.AddScoped<IGreeterService, GreeterService>();
        }

        /// <summary>
        /// Метод для настройки конвейера HTTP-запросов.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGrpcService<GreeterService>();
            });
        }
    }
}
