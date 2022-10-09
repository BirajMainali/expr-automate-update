using expr_automate_updates.Constants;
using expr_automate_updates.Provider;
using expr_automate_updates.Resolver;

namespace expr_automate_updates.Worker;

public class WorkerService
{
    private readonly AppConfigurationProvider _configurationProvider;
    private readonly IISResolver _iisResolver;
    private readonly ReleaseDirectoryResolver _directoryResolver;

    public WorkerService(AppConfigurationProvider configurationProvider, IISResolver iisResolver,
        ReleaseDirectoryResolver directoryResolver)
    {
        _configurationProvider = configurationProvider;
        _iisResolver = iisResolver;
        _directoryResolver = directoryResolver;
    }

    public async Task BeginUpdate()
    {
        var siteName = _configurationProvider.Get(AppConfigConstants.Site);
        await _iisResolver.StopSite(siteName);
        _directoryResolver.CopyIgnoreDirectoryBeforeFlush();
        _directoryResolver.FlushPreviousRelease();
        _directoryResolver.ReplaceRelease();
        await _iisResolver.StartSite(siteName);
    }
}