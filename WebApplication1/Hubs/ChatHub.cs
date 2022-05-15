using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, string> _connections;

        public ChatHub(IDictionary<string, string> Connections)
        {
            _connections = Connections;
        }

        public override async Task OnConnectedAsync()
        {
            
            await base.OnConnectedAsync();
        }

        public async Task Connect(string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
            if (!_connections.TryGetValue(username, out _))
            {
                _connections.Add(username, Context.ConnectionId);
            }
        }

        public async Task Disconnect(string username)
        {
            if (_connections.TryGetValue(username, out _))
            {
                _connections.Remove(username);
            }
        }

        public class SignalMessagePayload
        {
            public string? from { get; set; }
            public string? to { get; set; }
            public string? content { get; set; }
        }
        public async Task MessageReceived(string to)
        {
            // if is connected
            await Clients.Group(to).SendAsync("MessageReceived");
            //await Clients.Client(Context.ConnectionId).SendAsync("SignalMessage", from, content);
            //if (_connections.TryGetValue(to, out string? connectionId))
            //{
            //    await Clients.Client(connectionId).SendAsync("SignalMessage", from, content);
            //}
        }
    }
}
