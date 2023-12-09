using ServerConfigurations.Configurations;

namespace Configurations
{
    public class ServerConfigurations
    {
        public ServerStatisticsConfig? ServerStatisticsConfig { get; set; }
        public MongoConnection? MongoConnection { get; set; }
    }
}