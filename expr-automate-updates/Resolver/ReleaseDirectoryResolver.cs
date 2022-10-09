using expr_automate_updates.Constants;
using expr_automate_updates.Provider;

namespace expr_automate_updates.Resolver;

public class ReleaseDirectoryResolver
{
    private readonly AppConfigurationProvider _configurationProvider;

    public ReleaseDirectoryResolver(AppConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }

    public void CopyIgnoreDirectoryBeforeFlush()
    {
        var releaseDir = _configurationProvider.Get(AppConfigConstants.ReleaseDir);
        var ignoreDir = _configurationProvider.Get(AppConfigConstants.IgnoreDir);
        var preventPath = Path.Combine(releaseDir, ignoreDir);
        var tempDir = _configurationProvider.Get(AppConfigConstants.TempDir);
        if (!Directory.Exists(preventPath)) return;
        if (Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);
        Directory.Move(preventPath, tempDir);
    }

    public void FlushPreviousRelease()
    {
        var flushPath = _configurationProvider.Get(AppConfigConstants.PreviousReleaseDir);
        if (!Directory.Exists(flushPath)) throw new Exception("Release directory does not exist");
        Directory.Delete(flushPath, true);
    }

    public void ReplaceRelease()
    {
        var releaseDir = _configurationProvider.Get(AppConfigConstants.ReleaseDir);
        var path = _configurationProvider.Get(AppConfigConstants.PreviousReleaseDir);
        var tempDir = _configurationProvider.Get(AppConfigConstants.TempDir);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        Directory.Move(releaseDir, path);
        Directory.Move(tempDir, Path.Combine(path, "/"));
        Directory.Delete(tempDir);
    }
}