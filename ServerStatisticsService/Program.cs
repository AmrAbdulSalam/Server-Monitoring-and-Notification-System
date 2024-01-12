using Configurations;

namespace ServerStatisticsService
{
    public class Program
    {
        static async Task Main()
        {
            string? path = @"D:\intership\ServerMonitoringAndNotificationSystem\appsettings.json";
            while (true)
            {
                await ReadConfigurations.ReadSettingsFile(path);
                var systemDiagnostic = new SystemDiagnostic();
                var serverInfo = systemDiagnostic.UpdateServerInfo();
                Console.WriteLine(serverInfo.ToString());
                Console.WriteLine(ReadConfigurations.Configurations.ServerStatisticsConfig.ServerIdentifier);
                var samplingIntervalSeconds = ReadConfigurations.Configurations.ServerStatisticsConfig.SamplingIntervalSeconds*1000;
                Thread.Sleep(samplingIntervalSeconds);
            }

        }
    }
}