using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            StartClientAsync();
            Console.ReadLine();
        }

        public static async Task StartClientAsync()
        {
            using (TcpClient client = new TcpClient())
            {
                client.NoDelay = true;
                client.LingerState = new LingerOption(true, 0);

                Console.WriteLine("Connecting to server.");
                await client.ConnectAsync("127.0.0.1", 1234);

                Console.WriteLine("Sending message.");
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = Encoding.UTF8.GetBytes("This message is longer than what the server is willing to read.");
                    await stream.WriteAsync(buffer, 0, buffer.Length);

                    Console.WriteLine("Reading message.");
                    int len = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, len);

                    Console.WriteLine("Incoming message: {0}", message);
                }
            }
        }
    }
}