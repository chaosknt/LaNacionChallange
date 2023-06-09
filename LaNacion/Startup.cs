using LaNacion.Common;
using LaNacion.Data;
using LaNacion.Data.Service;
using LaNacion.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace LaNacion
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

            services.AddDataServices();

            services.AddConfigurationService(Configuration);

            //services.AddDbContext<LaNacionContext>(options => options
            //  .UseSqlServer(Configuration.GetConnectionString("LaNacionDbConn")));

            services.AddDbContext<LaNacionContext>(options => options
                .UseInMemoryDatabase(databaseName: "LaNacionInMemory")
                );

            services.AddSingleton(Log.Logger);

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<CustomOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Program.APP_NAME, Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LaNacionContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", Program.APP_NAME);
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //context.Database.Migrate();

            if (env.IsDevelopment())
            {
                Seed.Init(app);
            }
        }
    }
}
