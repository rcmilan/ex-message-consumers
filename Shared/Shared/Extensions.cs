using System.Text;

namespace Shared
{
    public static class Extensions
    {

        public static byte[] EncodeMessage<T>(this T message)
        {
            var serializedMessage = System.Text.Json.JsonSerializer.Serialize(message);
            var encodedMessage = Encoding.UTF8.GetBytes(serializedMessage);

            return encodedMessage;
        }
    }
}
