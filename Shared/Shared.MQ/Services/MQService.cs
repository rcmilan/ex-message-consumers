using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared.MQ.Services
{
    internal class MQService : IMQService
    {
        private readonly IModel _model;
        private readonly IBasicProperties _properties;

        public MQService(IModel model)
        {
            _model = model;

            _properties = _model.CreateBasicProperties();
            _properties.Persistent = true;
        }

        public void BasicAck(ulong deliveryTag)
        {
            _model.BasicAck(deliveryTag, multiple: false);
        }

        public void BasicConsume(string exchange, IBasicConsumer consumer, bool autoAck = false)
        {
            _model.BasicConsume(exchange, autoAck, consumer);
        }

        public EventingBasicConsumer CreateEventingBasicConsumer(string exchange)
        {
            DeclareQueue(exchange);

            return new EventingBasicConsumer(_model);
        }

        public void EnsureExchange(string exchange, string exchangeType = ExchangeType.Direct, IDictionary<string, object> exchangeProperties = null)
        {
            _model.ExchangeDeclare(exchange, exchangeType, true, false, exchangeProperties);

            DeclareQueue(exchange);

            _model.QueueBind(exchange, exchange, "", new Dictionary<string, object>());
        }

        public T Publish<T>(string exchange, T message) where T : IBaseEvent
        {
            EnsureExchange(exchange);

            var encodedMessage = message.EncodeMessage();

            _model.BasicPublish(exchange, "", true, _properties, encodedMessage);

            return message;
        }

        private void DeclareQueue(string queue)
        {
            _model.QueueDeclare(queue, durable: true, autoDelete: false, exclusive: false);
        }
    }
}