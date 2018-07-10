# Blazor Chat Sample

This application demonstrates the use of [SignalR](https://www.asp.net/signalr) to create 
a [Blazor](https://blazor.net) chat application.

At the present time, Blazor (version 0.4) cannot use the .NET Core SignalR client library 
([see issue #20](https://github.com/aspnet/Blazor/issues/20)), so this application uses the
[Blazor Javascript interop library](https://blazor.net/docs/javascript-interop.html) to talk
to the SignalR Javascript client.

## Demo

A demo application is available at https://blazorchatsample.azurewebsites.net 

## About

At present, the .net standard SignalR client does not work with Blazor and WebAssembly. This is being
worked on, but in the meantime I decided to create a test Blazor app that uses the [SignalR Javascript 
client](https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-2.1).

### Technical Challenges 

Using the SignalR client in Javascript is very simple, but it's more complicated when we want to 
interface it to Blazor. We want to initiate and track the client in Blazor, but we can't return 
Javascript objects such as `connection` to Web Assembly code.

The solution I used is to pass a GUID string as a key to the interop library `chatClient.js` so 
the Blazor client can reference the connection via the key.

#### Callbacks

The other complication is handling of incoming messages. The message is handled by `signalr.min.js`
which passes it to `chatClient.js` which has registered a handler for the "ReceiveMessage" event.
```
    // create an inbound message handler for the "ReceiveMessage" event
    connection.on("ReceiveMessage", (username, message) => {
        console.log("Connection message received for " + key + " from " + username);
        // invoke Blazor dotnet method 
        // we pass the key in so we know which client received the message
        Blazor.invokeDotNetMethod(callback, key, "ReceiveMessage", username, message);
    });
```
This invokes `Blazor.invokeDotNetMethod` which has the callback details that were passed 
when `ChatClient.Start` was invoked. This has to be a static method, so we pass the key of
the connection back to Blazor to tell it which client received the message.

### Improvements & Suggestions

If you have any improvements or suggestions please submit as issues/pull requests on the Github repo.


