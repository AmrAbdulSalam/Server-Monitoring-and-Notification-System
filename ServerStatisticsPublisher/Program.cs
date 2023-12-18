using Configurations;
using RabbitMQ.Client;
using ServerStatisticsService;

namespace ServerStatisticsPublisher
{
    public class Program
    {
        public static async Task Main(string[] args) 
        {
            string? path = @"D:\intership\ServerMonitoringAndNotificationSystem\appsettings.json";

            await ReadConfigurations.ReadSettingsFile(path);

            var factory = new ConnectionFactory
            {
                Uri = new Uri(ReadConfigurations.Configurations.RabbitMQConfig.RabbitMQUrl)
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var topicPublisher = new TopicPublisher(channel , ReadConfigurations.Configurations.RabbitMQConfig);

            while (true)
            {
                var systemDiagnostic = new SystemDiagnostic();
                var message = systemDiagnostic.UpdateServerInfo();

                var serverIdentifier = ReadConfigurations.Configurations.ServerStatisticsConfig.ServerIdentifier;
                var samplingIntervalSeconds = ReadConfigurations.Configurations.ServerStatisticsConfig.SamplingIntervalSeconds * 1000;

                topicPublisher.Publish(message , serverIdentifier);

                Thread.Sleep(samplingIntervalSeconds);
            }
        }
    }
}