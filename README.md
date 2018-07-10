# Blazor Chat Sample

This application demonstrates the use of [SignalR](https://www.asp.net/signalr) to create 
a [Blazor](https://blazor.net) chat application.

At the present time, Blazor (version 0.4) cannot use the .NET Core SignalR client library 
([see issue #20](https://github.com/aspnet/Blazor/issues/20)), so this application uses the
[Blazor Javascript interop library](https://blazor.net/docs/javascript-interop.html) to talk
to the SignalR Javascript client.
