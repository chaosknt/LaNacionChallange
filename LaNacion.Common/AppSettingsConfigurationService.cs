using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace LaNacion.Common
{
    public class AppSettingsConfigurationService
    {
        private readonly IConfiguration configuration;

        public AppSettingsConfigurationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ImagesOutputPath => this.GetSetting("ProfileImage:ImagesOutputPath");

        public IEnumerable<string> AvailableImageExtensions => this.GetSetting("ProfileImage:AvailableImageExtensions").Split(",").ToArray();

        public int ImageMaxAllowedSizeInMB => this.configuration.GetValue<int>("ProfileImage:ImageMaxAllowedSizeInMb") * 1024 * 1024;

        private string GetSetting(string key)
        {
            return this.configuration[key];
        }

        public T GetSetting<T>(string key)
        {
            return this.configuration.GetValue<T>(key);
        }
    }
}