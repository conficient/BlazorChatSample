using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatSample.Server.Hubs
{
    /// <summary>
    /// The SignalR hub 
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// Send a message to all clients
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }

        /// <summary>
        /// Log connection
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Log disconnection
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message}");
            return base.OnDisconnectedAsync(e);
        }


    }
}
