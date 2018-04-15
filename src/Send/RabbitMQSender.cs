using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Send
{
    public class RabbitMQSender
    {
        private IConfiguration _configuration;
        private string HostName { get; set; }
        public RabbitMQSender(IConfiguration configuration)
        {
            _configuration = configuration;
            HostName = _configuration["hostname"];
        }

        public async Task SendAsync(string profile, byte[] message)
        {
            var queue = _configuration[$"rabbitPublishProfiles:{profile}:queue"];
            var routingKey = _configuration[$"rabbitPublishProfiles:{profile}:routingKey"];
            await SendAsync(queue, routingKey, message);
        }

        public async Task SendAsync(string queue, string routingKey,byte[] message)
        {
            var factory = new ConnectionFactory() { HostName = HostName };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    String s = Convert.ToBase64String(message);
                    channel.BasicPublish(exchange: "", routingKey: routingKey,
                        basicProperties: null, body: message);
                    Console.WriteLine(" [x] Sent {0}", s);
                }
            }
        }
    }
}