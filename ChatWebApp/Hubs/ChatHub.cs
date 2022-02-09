using Microsoft.AspNetCore.SignalR;

namespace ChatWebApp.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string sender, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", sender, message);
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task SendMessageToGroup(string group, string sender, string message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", sender, message);
        }
    }
}