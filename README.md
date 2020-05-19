# Blazor Chat Sample

[![Build Status](https://dev.azure.com/conficient/BlazorChatSample/_apis/build/status/conficient.BlazorChatSample?branchName=master)](https://dev.azure.com/conficient/BlazorChatSample/_build/latest?definitionId=2&branchName=master)

> Now upgraded for [.NET core 3.2.0 rtm](https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-now-available/) - 
> please ensure you have installed the latest Blazor templates and VS 2019 preview.

This application demonstrates the use of [SignalR](https://www.asp.net/signalr) 
to create a [Blazor](https://blazor.net/) chat application.

### Now JavaScript-Free!

The app now uses the `Microsoft.AspNetCore.SignalR.Client` 
library which is now compatible with the Mono WASM runtime. This really simplifies the 
`ChatClient` code.

Previously this sample used JavaScript SignalR client. If you want to see how the JavaScript client version worked, I've retained 
it in [this branch](https://github.com/conficient/BlazorChatSample/tree/netcore-3.2.0-preview1)

## Demo

A demo application is available at https://blazorchatsample.azurewebsites.net 

### Improvements & Suggestions

If you have any improvements or suggestions please submit as issues/pull requests on the Github repo.

### Acknowledgements

Thanks to Code-Boxx for the article https://code-boxx.com/responsive-css-speech-bubbles/ 
that helped me create simple CSS speech bubbles that improve the layout.
