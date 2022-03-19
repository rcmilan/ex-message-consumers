using RabbitMQ.Client;

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

        public T Publish<T>(string exchange, T message) where T : IBaseEvent
        {
            EnsureExchange(exchange);

            var encodedMessage = message.EncodeMessage();

            _model.BasicPublish(exchange, "", true, _properties, encodedMessage);

            return message;
        }


        public void EnsureExchange(string exchange, string exchangeType = ExchangeType.Direct, IDictionary<string, object> exchangeProperties = null)
        {
            _model.ExchangeDeclare(exchange, exchangeType, true, false, exchangeProperties);

            _model.QueueDeclare(exchange, durable: true, autoDelete: false, exclusive: false);

            _model.QueueBind(exchange, exchange, "", new Dictionary<string, object>());
        }
    }
}