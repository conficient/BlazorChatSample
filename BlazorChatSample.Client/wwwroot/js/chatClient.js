// BlazorChat Javascript interop to SignalR

// We cannot pass Javascript objects back to Blazor so this library will store
// each connection created using a key in the `connections` object.
// 
var connections = {};

Blazor.registerFunction('ChatClient.Start',

    // key: key to use to access the SignalR client created
    // hubUrl: url to the chat hub
    // callback: defines a Blazor method to call when incoming messages are received
    // callbackAssembly: assembly that contains the Blazor code to call
    // callbackClass:    the class containing the callback method
    // callbackMethod:   the method to call when
    function (key, hubUrl, callbackAssembly, callbackClass, callbackMethod) {
        // key is the unique key we use to store/retrieve connections
        console.log("Connection start");

        // set up callback for received messages
        var callback = {
            type: { assembly: callbackAssembly, name: callbackClass },
            method: { name: callbackMethod }
        };

        // create a client
        console.log("Connection being started for " + hubUrl);
        var connection = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl)
            .build();

        console.log("Connection created, adding receive handler");

        // create an inbound message handler for the "ReceiveMessage" event
        connection.on("ReceiveMessage", (username, message) => {
            console.log("Connection message received for " + key + " from " + username);
            // invoke Blazor dotnet method 
            // we pass the key in so we know which client received the message
            Blazor.invokeDotNetMethod(callback, key, "ReceiveMessage", username, message);
        });

        // start the connection
        connection.start();
        // store connection in our lookup object
        connections[key] = connection;
    });

// 
// function called when Blazor client wishes to send a message via SignalR
//
Blazor.registerFunction('ChatClient.Send', function (key, username, message) {
    console.log("Connection send request");
    var connection = connections[key];
    if (!connection) throw "Connection not found for " + key;
    console.log("Connection located");
    // send message
    connection.invoke("SendMessage", username, message);
    // dummy
    return "ok";
});

//
// close and dispose of a connection
//
Blazor.registerFunction('ChatClient.Stop', function (key) {
    console.log("Connection stop request: " + key);
    // locate the SignalR connection
    var connection = connections[key];
    if (connection) {
        connection.stop();
        console.log("Connection stopped");
        // remove refs
        delete connections[key];
        connection = null;
    }
    else
        console.log("Connection not found for " + key);
});

