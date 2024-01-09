using RabbitMQ.Client;

namespace ScienceFestival.Async.Persistance
{
    public interface IMessageBroker
    {
        void Publish<T>(T message);
    }
}
