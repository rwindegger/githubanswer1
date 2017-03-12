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
            StartServerAsync();
            Console.ReadLine();
        }

        public static async Task StartServerAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 1234);
            listener.Server.NoDelay = true;
            listener.Server.LingerState = new LingerOption(true, 0);
            listener.Start();

            while (true)
            {
                Console.WriteLine("Waiting for client.");
                using (TcpClient client = await listener.AcceptTcpClientAsync())
                {
                    client.NoDelay = true;
                    client.LingerState = new LingerOption(true, 0);

                    Console.WriteLine("Reading message.");
                    using (NetworkStream stream = client.GetStream())
                    {
                        StringBuilder sb = new StringBuilder();
                        byte[] buffer = new byte[32];
                        int len;
                        do
                        {
                            len = await stream.ReadAsync(buffer, 0, buffer.Length);
                            sb.Append(Encoding.UTF8.GetString(buffer, 0, len));
                        } while (len == buffer.Length);
                        Console.WriteLine("Incoming message: {0}", sb.ToString());
                        
                        Console.WriteLine("Sending message.");
                        byte[] message = Encoding.UTF8.GetBytes("Thank you!");
                        await stream.WriteAsync(message, 0, message.Length);
                        Console.WriteLine("Closing connection.");
                    }
                }
            }
        }
    }
}