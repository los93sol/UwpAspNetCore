using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace UwpAspNetCore
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            var host = WebHost
                .CreateDefaultBuilder()
                .UseSetting("preventHostingStartup", "true")
                .UseStartup<UwpStartup>()
                .UseUrls("http://+:80")
                .Build();

            host.Run();
        }
    }
}