using ChessLogic;
using Microsoft.AspNetCore.SignalR;

namespace ChessServer.Hubs
{
    public class ChessMoveHub : Hub
    {
        public async Task OnMakeMove(string move)
        {
            await Clients.OthersInGroup("default").SendAsync("OnReceiveMove", move);
        }

        public async override Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "default");
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "default");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
