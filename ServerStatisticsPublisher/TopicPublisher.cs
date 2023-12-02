using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Configurations;
using ServerStatisticsService;

namespace ServerStatisticsPublisher
{
    public static class TopicPublisher
    {
        public static async Task Publish(IModel channel)
        {
            channel.ExchangeDeclare("topic-ServerStatistics-exchange" , ExchangeType.Topic);
            string? path = @"D:\intership\ServerMonitoringAndNotificationSystem\appsettings.json";

            while (true)
            {
                await ReadConfigurations.ReadSettingsFile(path);

                var systemDiagnostic = new SystemDiagnostic();
                var message = systemDiagnostic.UpdateServerInfo();

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
  
                var serverIdentifier = ReadConfigurations.Configurations.ServerStatisticsConfig.ServerIdentifier;
                var samplingIntervalSeconds = ReadConfigurations.Configurations.ServerStatisticsConfig.SamplingIntervalSeconds*1000;

                Console.WriteLine(message.ToString());
                channel.BasicPublish("topic-ServerStatistics-exchange", $"ServerStatistics.{serverIdentifier}", null, body);
                Thread.Sleep(samplingIntervalSeconds);
            }
        }
    }
}