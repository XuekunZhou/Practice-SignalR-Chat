"use strict";
const uri = '../../api/chatmessage';
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

var chatId = document.getElementById("chatId").value;
//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

var box = document.getElementById("chatBody");

function AddMessage(sender, message) {
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
    var window = document.getElementById("chatbody");
    card.appendChild(cardBody);
    cardBody.appendChild(text);
    cardBody.appendChild(date);
    date.appendChild(datetext);
    window.appendChild(row);

    text.textContent = `${message}`;
    datetext.textContent = new Date().toUTCString();
    window.scrollTop = window.scrollHeight;
}

connection.on("ReceiveMessage", function (sender, message) {
    SendMessageToDatabase(sender, message, chatId);
    AddMessage(sender, message);
    document.getElementById("messageInput").value = "";
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("JoinGroup", chatId).catch(function (err)
    {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    var sender = document.getElementById("sender").value;
    connection.invoke("SendMessageToGroup", chatId, sender, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function SendMessageToDatabase(sender, message, chat) {
    var date = Date();
  
    const item = {
      SenderId: sender,
      Message: message,
      ChatId: chat
    };

    fetch(uri, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
      })
        .then(response => response.json())
        .then(() => {
            getItems();
          })
        .catch(error => console.error('Unable to add item.', error));
}

function getItems() {
    fetch(uri)
      .then(response => response.json())
      .then(data => _displayItems(data))
      .catch(error => console.error('Unable to get items.', error));
  }