using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ScienceFestival.Async.Persistance
{
    public class MessageBroker : IMessageBroker
    {
        public void Publish<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "reviews",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish(exchange: "",
                                 routingKey: "reviews",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
