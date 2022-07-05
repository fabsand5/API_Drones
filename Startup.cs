using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ws_API_Drones.Services;
using System.IO;
using API_Drones;
using Microsoft.OpenApi.Models;
using API_Drones.Services;

namespace ws_API_Drones
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // BD
            services.AddDbContext<DataContext.AppContext>(options =>
                          options.UseSqlServer(
                              Configuration.GetConnectionString("DefaultConnection")));
            //Register dapper in scope    
            services.AddScoped<IDapper, Dapperr>();

            services.AddControllersWithViews();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            // add swagger documentación
            AddSwagger(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Drones v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // use cors
            app.UseCors("MyPolicy");


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "API REST DRONES");
            });
        }


        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1.0";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"API DRONES {groupName}",
                    Version = groupName,
                    Description = "API DRONES",
                    Contact = new OpenApiContact
                    {
                        Name = "Fabian Sandoval",
                        Email = "fabian.sandoval.estay@gmail.com"
                    }
                });
            });
        }
    }
}
