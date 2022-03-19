using RabbitMQ.Client.Events;
using System.Text;

namespace Shared.MQ
{
    public static class Extensions
    {
        public static T DeserializeDeliveredEventArg<T>(this BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var message = System.Text.Json.JsonSerializer.Deserialize<T>(json);

            return message!;
        }
    }
}
