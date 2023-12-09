using ServerConfigurations.Configurations;

namespace Configurations
{
    public class ServerConfigurations
    {
        public ServerStatisticsConfig? ServerStatisticsConfig { get; set; }
        public MongoConnection? MongoConnection { get; set; }
        public AnomalyDetectionConfig? AnomalyDetectionConfig { get; set; }
        public SignalRConfig? SignalRConfig { get; set; }
    }
}