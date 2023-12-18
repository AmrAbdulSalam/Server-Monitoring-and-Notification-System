using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ServerStatisticsService;
using ServerConfigurations.Configurations;

namespace ServerStatisticsPublisher
{
    public class TopicPublisher : ITopicPublisher
    {
        private readonly IModel _channel;
        private readonly RabbitMQConfig _config;

        public TopicPublisher(IModel channel , RabbitMQConfig config)
        {
            _channel = channel;
            _config = config;
        }

        public void Publish(ServerStatisticsDTO message , string serverIdentifier)
        {
            _channel.ExchangeDeclare(_config.Exchange, ExchangeType.Topic);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            Console.WriteLine(message.ToString());

            _channel.BasicPublish(_config.Exchange, $"ServerStatistics.{serverIdentifier}", null, body);

        }
    }
}