using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LaNacion.Common
{
    public static class ConfigueServices
    {
        public static IServiceCollection AddConfigurationService(this IServiceCollection services, IConfiguration configuration)
           => services
               .AddSingleton<AppSettingsConfigurationService>();
    }
}