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

        public static void Loader(int ndots = 3)
        {
            for (int dots = 0; dots < ndots; dots++)
            {
                Console.Write(".");

                Thread.Sleep(500);
            }

            Console.WriteLine("\nConcluido!");
        }
    }
}