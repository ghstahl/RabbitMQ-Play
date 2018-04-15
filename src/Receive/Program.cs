using System;
using System.IO;
using System.Text;
using benchmarks.proto2;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "proto", durable: false, exclusive: false, autoDelete: false,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        Stream stream = new MemoryStream(body);
                        var gm2 = ProtoBuf.Serializer.Deserialize<GoogleMessage2>(stream);
                        string message = JsonConvert.SerializeObject(gm2, Formatting.Indented);
                       
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: "proto", autoAck: true, consumer: consumer);



                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }

            }
        }
    }
}
