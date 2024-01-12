using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerConfigurations.Configurations;
using ServerStatisticsConsumer.Services;
using ServerStatisticsService;

namespace ServerStatisticsConsumer
{
    public class TopicConsumer : ITopicConsumer
    {
        private readonly IModel _channel;
        private readonly IMongoDB _mongoDB;
        private readonly RabbitMQConfig _config;
        private readonly AlertCalculations _alertCalculation;

        public TopicConsumer(IModel channel, IMongoDB mongoDB, RabbitMQConfig config , AlertCalculations alertCalculation)
        {
            _channel = channel;
            _mongoDB = mongoDB;
            _config = config;
            _alertCalculation = alertCalculation;
        }

        public async Task Consume()
        {
            _channel.ExchangeDeclare(_config.Exchange, ExchangeType.Topic);

            _channel.QueueDeclare(_config.Queue,
               durable: true,
               exclusive: false,
            autoDelete: false,
            arguments: null);

            _channel.QueueBind(_config.Queue, _config.Exchange, _config.RoutingKey);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var serverStatDto = JsonConvert.DeserializeObject<ServerStatisticsDTO>(message);
                var serverStat = new ServerStatistics
                {
                    ServerIdentifier = e.RoutingKey,
                    MemoryUsage = serverStatDto.MemoryUsage,
                    AvailableMemory = serverStatDto.AvailableMemory,
                    CpuUsage = serverStatDto.CpuUsage,
                    Timestamp = serverStatDto.Timestamp
                };

                await _alertCalculation.SendAlert(serverStatDto);
                await _mongoDB.Insert(serverStat);
            };
            _channel.BasicConsume(_config.Queue, true, consumer);
            Console.WriteLine("Consumer Started");
            Console.ReadLine();
        }
    }
}