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

        public static void Loader(int ndots = 3, bool success = true)
        {
            for (int dots = 0; dots < ndots; dots++)
            {
                Console.Write(".");

                Thread.Sleep(500);
            }

            if (success)
                Console.WriteLine("\nConcluido!!\n");
            else
                Console.WriteLine("\nERRO!!\n");
        }

        public static bool FakeProcess(int min = 3, int max = 15)
        {
            var random = new Random();

            var ndots = random.Next(min, max);

            var success = ndots % 2 == 0;

            Loader(ndots, success);

            return success;
        }
    }
}