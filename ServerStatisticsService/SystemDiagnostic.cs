using System.Diagnostics;

namespace ServerStatisticsService
{
    public class SystemDiagnostic
    {
        public ServerStatisticsDTO UpdateServerInfo()
        {
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            var cpuUsage = Math.Round(cpuCounter.NextValue() , 2);
            var currentProcess = Process.GetCurrentProcess();
            double memoryUsage = Math.Round((double)(currentProcess.WorkingSet64/(1024*1024)),2);
            var serverStatistics = new ServerStatisticsDTO
            {
                MemoryUsage = memoryUsage,
                AvailableMemory = 100-memoryUsage,
                CpuUsage = cpuUsage,
                Timestamp = DateTime.Now
            };

            return serverStatistics;
        }
    }
}