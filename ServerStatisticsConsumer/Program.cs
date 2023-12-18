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
            string? path = @"D:\intership\ServerMonitoringAndNotificationSystem\appsettings.json";

            await ReadConfigurations.ReadSettingsFile(path);

            var factory = new ConnectionFactory
            {
                Uri = new Uri(ReadConfigurations.Configurations.RabbitMQConfig.RabbitMQUrl)
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var mongoConnectionString = ReadConfigurations.Configurations.MongoConnection.ConnectionString;
            var mongoDatabase = ReadConfigurations.Configurations.MongoConnection.Database;
            var mongoCollection = ReadConfigurations.Configurations.MongoConnection.Collection;

            var mongoDB = new MongoDBService(mongoConnectionString , mongoDatabase , mongoCollection);
            var serverAlertService = new ServerAlertSenderService(ReadConfigurations.Configurations.SignalRConfig.SignalRUrl);

            var alertCalculation = new AlertCalculations(ReadConfigurations.Configurations.AnomalyDetectionConfig, serverAlertService);

            var topicConsumer = new TopicConsumer(channel , mongoDB , ReadConfigurations.Configurations.RabbitMQConfig, alertCalculation);

            await topicConsumer.Consume();
        }
    }
}