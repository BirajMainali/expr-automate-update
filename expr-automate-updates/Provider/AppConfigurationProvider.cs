namespace expr_automate_updates.Provider;

public class AppConfigurationProvider
{
    private readonly IConfiguration _configuration;

    public AppConfigurationProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Get(string key)
        => _configuration.GetSection("Configuration").GetSection(key).Value;
}