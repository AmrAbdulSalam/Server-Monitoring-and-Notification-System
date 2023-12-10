using RabbitMQ.Client;
using ServerStatisticsConsumer.Services;
using Configurations;
using SignalRService;

namespace ServerStatisticsConsumer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            string? path = @"D:\intership\ServerMonitoringAndNotificationSystem\appsettings.json";
            await ReadConfigurations.ReadSettingsFile(path);

            var mongoConnectionString = ReadConfigurations.Configurations.MongoConnection.ConnectionString;
            var mongoDatabase = ReadConfigurations.Configurations.MongoConnection.Database;
            var mongoCollection = ReadConfigurations.Configurations.MongoConnection.Collection;

            var mongoDB = new MongoDBService(mongoConnectionString , mongoDatabase , mongoCollection);
            var serverAlertService = new ServerAlertService(ReadConfigurations.Configurations.SignalRConfig.SignalRUrl);

            await TopicConsumer.Consume(channel , mongoDB
                , ReadConfigurations.Configurations.AnomalyDetectionConfig
                , serverAlertService);
        }
    }
}