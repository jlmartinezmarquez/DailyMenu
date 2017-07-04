using System.Configuration;

namespace DailyMenu.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public string this[string key] => Get(key);

        public string Get(string key)
        {
            return GetConnectionString(key)
                ?? GetAppSetting(key);
        }

        private static string GetAppSetting(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                return null;
            }
        }

        private string GetConnectionString(string key)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[key]?.ConnectionString;
            }
            catch
            {
                return null;
            }
        }
    }
}
