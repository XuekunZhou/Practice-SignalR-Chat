using Microsoft.AspNetCore.SignalR;

namespace ChatWebApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}