using LaNacion;
using LaNacion.Data;
using LaNacion.Data.Service.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Linq;

namespace LaNatacion.Api.Tests
{
    public class TestServiceWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public static readonly InMemoryDatabaseRoot memoryDatabase = new InMemoryDatabaseRoot();

        public IContactManager ContactManager => this.Services.GetRequiredService<IContactManager>();

        public ILogger Logger => this.Services.GetRequiredService<ILogger>();

        public LaNacionContext Database => this.Services.GetRequiredService<LaNacionContext>();

        public void Init(TestEntities entity)
        {
            using (var scope = this.Services.CreateScope())
            {
                var database = scope.ServiceProvider.GetRequiredService<LaNacionContext>();
            }

            switch (entity)
            {
                case TestEntities.Contact:
                    DatabaseHelper.InitContacts(Database);
                    break;
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseEnvironment("Unit Tests");
            builder.ConfigureServices(Services =>
            {
                var descriptor = Services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LaNacionContext>));
                if (descriptor != null) Services.Remove(descriptor);

                Services.AddEntityFrameworkInMemoryDatabase();
                Services.AddDbContext<LaNacionContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb", memoryDatabase);
                });

            });
        }
    }
}
