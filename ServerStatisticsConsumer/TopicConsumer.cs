using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerStatisticsConsumer.Services;
using ServerStatisticsService;

namespace ServerStatisticsConsumer
{
    public static class TopicConsumer
    {
        public static async Task Consume(IModel channel , IMongoDB mongoDB)
        {
            channel.ExchangeDeclare("topic-ServerStatistics-exchange", ExchangeType.Topic);
            channel.QueueDeclare("topic-ServerStatistics-queue",
               durable: true,
               exclusive: false,
            autoDelete: false,
            arguments: null);

            channel.QueueBind("topic-ServerStatistics-queue", "topic-ServerStatistics-exchange", "ServerStatistics.*");

            var consumer = new EventingBasicConsumer(channel);
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

                await mongoDB.Insert(serverStat);
            };
            channel.BasicConsume("topic-ServerStatistics-queue", true, consumer);
            Console.WriteLine("Consumer Started");
            Console.ReadLine();
        }
    }
}