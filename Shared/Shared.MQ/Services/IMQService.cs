using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared.MQ.Services
{
    public interface IMQService
    {
        void BasicAck(ulong deliveryTag);

        void BasicConsume(string exchange, IBasicConsumer consumer, bool autoAck = false);

        EventingBasicConsumer CreateEventingBasicConsumer(string exchange);

        void EnsureExchange(string exchange, string exchangeType = ExchangeType.Direct, IDictionary<string, object> exchangeProperties = null);

        T Publish<T>(string exchange, T message) where T : IBaseEvent;
    }
}