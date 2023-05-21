using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanSignalR
{
    public class SignalRClient
    {
        public const string URL = $"https://localhost:7228/ChatHub";
        public static HubConnection Connection { get; set; }

        public static async Task Connect()
        {
            try
            {
                Console.WriteLine("Connessione in corso...");
                if (Connection != null)
                    return;
                Connection = new HubConnectionBuilder()
                    .WithUrl(URL)
                    .WithAutomaticReconnect()
                    .Build();

                Connection.On<string, string>("ReceiveMessageFromAll",
                    (user, message) =>
                    {
                        Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} {user}> {message}");
                    });

                await Connection.StartAsync();
                Console.WriteLine("Connesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERRORE! {e}");
            }
        }
    }
}
