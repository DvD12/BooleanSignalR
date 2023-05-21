using Microsoft.AspNetCore.SignalR.Client;
using System.Text;

namespace BooleanSignalR
{
    internal class Program
    {
        public static string UserName { get; set; }
        private static StringBuilder Buffer = new StringBuilder();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Immetti il tuo username per entrare nella chat:");
            UserName = Console.ReadLine();
            await SignalRClient.Connect();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Enter:
                            Console.WriteLine();
                            await SignalRClient.Connection.InvokeAsync("SendMessageToAll", UserName, Buffer.ToString());
                            Buffer.Clear();
                            break;
                        default:
                            Buffer.Append(key.KeyChar);
                            Console.Write(key.KeyChar);
                            break;
                    }
                }
            }
        }
    }
}