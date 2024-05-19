using Microsoft.AspNetCore.SignalR;

namespace Easi_TaskDemo.Hubs
{
    public sealed class StreamingHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
        }
    }
}
