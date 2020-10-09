# Blazor Chat Sample

[![Build Status](https://dev.azure.com/conficient/BlazorChatSample/_apis/build/status/conficient.BlazorChatSample?branchName=master)](https://dev.azure.com/conficient/BlazorChatSample/_build/latest?definitionId=2&branchName=master)

> Now upgraded for [.NET 5 RC-1](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-5-release-candidate-1/) - many thanks [Turochamp](https://github.com/Turochamp)! Please ensure you have installed the latest Blazor templates and VS 2019 preview.

This application demonstrates the use of [SignalR](https://www.asp.net/signalr) 
to create a [Blazor](https://blazor.net/) chat application.

### Now JavaScript-Free!

The app now uses the `Microsoft.AspNetCore.SignalR.Client` 
library which is now compatible with the Mono WASM runtime. This really simplifies the 
`ChatClient` code.

Previously this sample used JavaScript SignalR client. If you want to see how the JavaScript client version worked, I've retained 
it in [this branch](https://github.com/conficient/BlazorChatSample/tree/netcore-3.2.0-preview1)

## .NET 5

@Turochamp has kindly provided an upgrade to .NET 5, which now uses CSS isolation for the app, navbar etc.

## Demo

A demo application is available at https://blazorchatsample.azurewebsites.net 

### Improvements & Suggestions

If you have any improvements or suggestions please submit as issues/pull requests on the Github repo.

### Acknowledgements

Many thanks to [Turochamp](https://github.com/Turochamp) who did the upgrade to .NET 5 !

Thanks to Code-Boxx for the article https://code-boxx.com/responsive-css-speech-bubbles/ 
that helped me create simple CSS speech bubbles that improve the layout.
