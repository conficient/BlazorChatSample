using BlazorChatSample.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace BlazorChatSample.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Console app
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            // pause for 3 seconds to allow the web host to boot up
            Console.WriteLine("Starting App...");
            try
            {
                string username;
                do
                {
                    Console.WriteLine("Enter your name: ");
                    username = Console.ReadLine();
                }
                while (string.IsNullOrWhiteSpace(username));

                // connect to host: the HTTPS version may not work on localhost as the local IIS cert isn't valid
                const string url = "http://localhost:6840";
                var client = new ChatClient(username, url);

                // create a message received handler
                client.MessageReceived += Client_MessageReceived;

                await client.StartAsync();
                bool exit = false;
                Console.WriteLine("Enter message, or 'exit' to quit");
                do
                {
                    var message = Console.ReadLine();
                    await client.SendAsync(message);

                    // either Ctrl-C or type 'exit' to quit
                    if (message?.ToLower() == "exit")
                        exit = true;

                } while (!exit);

                Console.WriteLine("press any key to exit");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }


        private static void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine($"[{e.Username}] {e.Message}");
        }
    }
}
