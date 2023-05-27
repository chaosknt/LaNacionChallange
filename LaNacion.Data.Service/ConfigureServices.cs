using LaNacion.Data.Service.Managers;
using LaNacion.Data.Service.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace LaNacion.Data.Service
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
            => services
                .AddDataManagerServices()
                .AddDataStoreServices();

        internal static IServiceCollection AddDataManagerServices(this IServiceCollection services)
            => services
                .AddScoped<IContactManager, ContactManager>();

        internal static IServiceCollection AddDataStoreServices(this IServiceCollection services)
          => services
              .AddScoped<IContactStore, ContactStore>()
              .AddScoped<IPhoneNumberStore, PhoneNumberStore>();
    }
}