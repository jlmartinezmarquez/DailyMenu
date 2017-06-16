namespace DailyMenu.Configuration
{
    public interface IConfigurationService
    {
        string Get(string key);
        string this[string key] { get; }
    }
}
