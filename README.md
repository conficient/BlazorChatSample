# Blazor Chat Sample

[![Build Status](https://dev.azure.com/conficient/BlazorChatSample/_apis/build/status/conficient.BlazorChatSample?branchName=master)](https://dev.azure.com/conficient/BlazorChatSample/_build/latest?definitionId=2&branchName=master)

> Now upgraded for [.NET 5 RTM](https://devblogs.microsoft.com/aspnet/announcing-asp-net-core-in-net-5/) - Please ensure you have the .NET 5 SDK loaded and VS 2019 v16.8 or later.
> One change since the release candidates is that the scoped CSS is now `AppName.styles.css` in place of the `_framework/scoped.styles.css`

This application demonstrates the use of [SignalR](https://www.asp.net/signalr) 
to create a [Blazor](https://blazor.net/) chat application.

### Now JavaScript-Free!

The app now uses the `Microsoft.AspNetCore.SignalR.Client` 
library which is now compatible with the Mono WASM runtime. This really simplifies the 
`ChatClient` code.

Previously this sample used JavaScript SignalR client. If you want to see how the JavaScript client version worked, I've retained 
it in [this branch](https://github.com/conficient/BlazorChatSample/tree/netcore-3.2.0-preview1)

## .NET 6

Upgraded the demo to .NET 6.

## Demo

A demo application is available at https://blazorchatsample.azurewebsites.net 

### Improvements & Suggestions

If you have any improvements or suggestions please submit as issues/pull requests on the Github repo.

### Acknowledgements

Thanks to Code-Boxx for the article https://code-boxx.com/responsive-css-speech-bubbles/ 
that helped me create simple CSS speech bubbles that improve the layout.
