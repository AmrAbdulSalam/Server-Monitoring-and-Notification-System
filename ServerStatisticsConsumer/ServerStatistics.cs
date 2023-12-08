
namespace ServerStatisticsConsumer
{
    public class ServerStatistics
    {
        public string? ServerIdentifier { get; set; }
        public double MemoryUsage { get; set; } // in MB
        public double AvailableMemory { get; set; } // in MB
        public double CpuUsage { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString()
           => $"FromServer = {ServerIdentifier} : " +
            $"MemoryUsage:{MemoryUsage}%,AvailableMemory:{AvailableMemory},CpuUsage:{CpuUsage},Time:{Timestamp}";
    }
}