using Microsoft.AspNetCore.Blazor.Browser.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatSample.Client
{

    /// <summary>
    /// Generic client class that interfaces .NET Standard/Blazor with SignalR Javascript client
    /// </summary>
    public class ChatClient : IDisposable
    {

        #region static methods

        /// <summary>
        /// internal dictionary of SignalRclients by Key
        /// </summary>
        private static Dictionary<string, ChatClient> _clients = new Dictionary<string, ChatClient>();


        /// <summary>
        /// Inbound message handler 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="method"></param>
        /// <param name="username"></param>
        /// <param name="message"></param>
        /// <remarks>
        /// This method is called from Javascript when amessage is received
        /// </remarks>
        public static void ReceiveMessage(string key, string method, string username, string message)
        {
            if (_clients.ContainsKey(key))
            {
                var client = _clients[key];
                client.HandleReceiveMessage(username, message);
            }
            else
            {
                // unable to match the message to a client
                Console.WriteLine($"ReceiveMessage: unable to find {key}");
            }
        }

        #endregion

        /// <summary>
        /// Ctor: create a new client for the given hub URL
        /// </summary>
        /// <param name="hubUrl"></param>
        public ChatClient(string username, string hubUrl)
        {
            // save the hub url
            _hubUrl = hubUrl ?? throw new ArgumentNullException(nameof(hubUrl));
            // save username
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            _username = username;
            // create a unique key for this client
            _key = Guid.NewGuid().ToString();
            // add to the list of clients
            _clients.Add(_key, this);
        }

        /// <summary>
        /// The Hub URL for this client
        /// </summary>
        private readonly string _hubUrl;

        /// <summary>
        /// Our unique key for this client instance
        /// </summary>
        /// <remarks>
        /// We cannot pass JS objects to Blazor/C# so we use a unique key
        /// to reference each instance. The JS client will store the object
        /// under our key so we can reference it
        /// </remarks>
        private readonly string _key;

        /// <summary>
        /// Flag to show if started
        /// </summary>
        private bool _started = false;

        /// <summary>
        /// Name of the chatter
        /// </summary>
        private string _username;


        /// <summary>
        /// Start the SignalR client on JS
        /// </summary>
        public void Start()
        {
            if (!_started)
            {
                // the callback values for inbound messages
                const string callbackAssembly = "BlazorChatSample.Client.dll";
                const string callbackClass = "BlazorChatSample.Client.ChatClient"; // include namespace
                const string callbackMethod = "ReceiveMessage"; // static method to call
                // invoke the JS interop start client method
                var tmp = RegisteredFunction.Invoke<bool>("ChatClient.Start", _key, _hubUrl,
                    callbackAssembly, callbackClass, callbackMethod);
                _started = true;
            }
        }

        /// <summary>
        /// Handle an inbound message from a hub
        /// </summary>
        /// <param name="method">event name</param>
        /// <param name="message">message content</param>
        private void HandleReceiveMessage(string username, string message)
        {
            // raise an event to subscribers
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }

        /// <summary>
        /// Event raised when this client receives a message
        /// </summary>
        /// <remarks>
        /// Instance classes should subscribe to this event
        /// </remarks>
        public event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Send a message to the hub
        /// </summary>
        /// <param name="message">message to send</param>
        public void Send(string message)
        {
            // check we are connected
            if (!_started)
                throw new InvalidOperationException("Client not started");
            // send the message
            var tmp = RegisteredFunction.Invoke<string>("ChatClient.Send", _key, _username, message);
        }

        /// <summary>
        /// Stop the client (if started)
        /// </summary>
        public void Stop()
        {
            if (_started)
            {
                // disconnect the client
                var tmp = RegisteredFunction.Invoke<bool>("ChatClient.Stop", _key);
                _started = false;
            }
        }

        /// <summary>
        /// Dispose of client
        /// </summary>
        public void Dispose()
        {
            // ensure we stop if connected
            if (_started) Stop();

            // remove this key from the list of clients
            if (_clients.ContainsKey(_key))
                _clients.Remove(_key);
        }
    }

    /// <summary>
    /// Delegate for the message handler
    /// </summary>
    /// <param name="sender">the SignalRclient instance</param>
    /// <param name="e">Event args</param>
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

    /// <summary>
    /// Message received argument class
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(string username, string message)
        {
            Username = username;
            Message = message;
        }

        /// <summary>
        /// Name of the message/event
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Message data items
        /// </summary>
        public string Message { get; set; }

    }

}

