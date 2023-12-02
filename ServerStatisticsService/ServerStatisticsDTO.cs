
namespace ServerStatisticsService
{
    public class ServerStatisticsDTO
    {
        public double MemoryUsage { get; set; } //MB
        public double AvailableMemory { get; set; } //MB
        public double CpuUsage { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString() 
            => $"MemoryUsage:{MemoryUsage}%,AvailableMemory:{AvailableMemory},CpuUsage:{CpuUsage},Time:{Timestamp}";
    }
}