using expr_automate_updates.Provider;
using expr_automate_updates.Resolver;
using expr_automate_updates.Worker;

namespace expr_automate_updates;

public static class DiConfig
{
    public static IServiceCollection UseConfiguration(this IServiceCollection service) 
        => service.AddScoped<WorkerService>()
            .AddScoped<IISResolver>()
            .AddScoped<ReleaseDirectoryResolver>()
            .AddScoped<AppConfigurationProvider>();
}