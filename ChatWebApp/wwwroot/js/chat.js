"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (sender, message) {
    var row = document.createElement("div");
    var colleft = document.createElement("div");
    var colright = document.createElement("div");

    var card = document.createElement("div");
    var cardBody = document.createElement("div");
    var text = document.createElement("p");
    var date = document.createElement("p");
    var datetext = document.createElement("SMALL");

    row.className = "row mt-2";
    colleft.className = "col-6";
    colright.className = "col-6";
    card.className = "card w-100";
    cardBody.className = "card-body";
    date.className = "card-text float-end";
    datetext.className = "text-muted";

    row.appendChild(colleft);
    row.appendChild(colright);

    var user = document.getElementById("sender").value;

    if (user == `${sender}`)
    {
        colright.appendChild(card);
    }
    else {
        colleft.appendChild(card);
    }
    card.appendChild(cardBody);
    cardBody.appendChild(text);
    cardBody.appendChild(date);
    date.appendChild(datetext);
    document.getElementById("chatbody").appendChild(row);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    text.textContent = `${message}`;
    datetext.textContent = Date();
    // datetext.textContent = `${sender}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    var sender = document.getElementById("sender").value;
    connection.invoke("SendMessage", sender, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});