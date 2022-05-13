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

        public async Task Connect(string username)
        {
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

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SignalMessage(string from, string to, string content)
        {
            // if is connected
            if (_connections.TryGetValue(to, out string? connectionId))
            {
                await Clients.Client(connectionId).SendAsync("SignalMessage", content);
            }
        }
    }
}
