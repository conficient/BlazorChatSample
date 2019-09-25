# Blazor Chat Sample

[![Build Status](https://dev.azure.com/conficient/BlazorChatSample/_apis/build/status/conficient.BlazorChatSample?branchName=master)](https://dev.azure.com/conficient/BlazorChatSample/_build/latest?definitionId=2&branchName=master)

> Now upgraded for [.NET core 3.0 rtm](https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0/)

This application demonstrates the use of [SignalR](https://www.asp.net/signalr) 
to create a [Blazor](https://blazor.net/) chat application.

At the present time, Blazor (.NET Core 3 rtm) cannot use the .NET Core SignalR client library 
([see issue #20](https://github.com/aspnet/Blazor/issues/20)), so this application uses the 
[Blazor Javascript interop](https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interop?view=aspnetcore-3.0)
capability to talk to the [SignalR Javascript client](https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-2.2).
        
This version of the app has been updated for the .NET Core 3.0 preview9.19465.2 version of Blazor WASM
(see https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.0#javascript-interop). 

## Demo

A demo application is available at https://blazorchatsample.azurewebsites.net 

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

### Acknowledgements

Thanks to Code-Boxx for the article https://code-boxx.com/responsive-css-speech-bubbles/ 
that helped me create simple CSS speech bubbles that improve the layout.
