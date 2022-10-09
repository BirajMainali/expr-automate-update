using System.Diagnostics;

namespace expr_automate_updates.Resolver;

public class IISResolver
{
    public async Task StopSite(string site)
    {
        var argument = $"/stop site \"{site}\"";
        await Execute(argument);
    }
    
    public async Task StartSite(string site)
    {
        var argument = $"/start site \"{site}\"";
        await Execute(argument);
    }

    private static async Task Execute(string argument)
    {
        var proc = new Process();
        proc.StartInfo.FileName = "cmd.exe";
        proc.StartInfo.Arguments = $"cd %windir%\\system32\\inetsrv\\ & appcmd.exe {argument}";
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.RedirectStandardError = true;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.CreateNoWindow = true;
        proc.Start();
        var output = await proc.StandardOutput.ReadToEndAsync();
        var error = await proc.StandardError.ReadToEndAsync();
        await proc.WaitForExitAsync();
        if (proc.ExitCode != 0)
        {
            proc.Close();
            throw new Exception("Error on iis: " + error);
        }

        proc.Close();
    }
}