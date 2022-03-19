using RabbitMQ.Client;

namespace Shared.MQ
{
    public class MQConfig
    {
        public IModel BuildModel => BuildConnection().CreateModel();

        private readonly string _hostname;

        public MQConfig(string hostname)
        {
            _hostname = hostname;
        }

        public IConnection BuildConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname
            };

            return factory.CreateConnection();
        }
    }
}