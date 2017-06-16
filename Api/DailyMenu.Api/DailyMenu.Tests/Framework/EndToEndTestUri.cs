using DailyMenu.Configuration;

namespace DailyMenu.Tests.Framework
{
    public class EndToEndTestUri
    {
        public static string GetBaseUri()
        {
            var configurationService = new ConfigurationService();

            return configurationService["Development.Uri"];
        }

        public static string GetResourceUri(string resource)
        {
            return $"{GetBaseUri()}/api/{resource}";
        }
    }
}
