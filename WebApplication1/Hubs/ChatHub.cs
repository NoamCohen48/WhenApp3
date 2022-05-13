using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class ChatHub:Hub
    {
        public async Task<string> SendMessage(string message)
        {
            await Clients.All.SendAsync();
        }
    }
}
