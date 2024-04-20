using Microsoft.AspNetCore.SignalR.Client;
using System.Net;

namespace ChessLogic
{
    public delegate void MakeMoveEvent(Move move, bool fromNetwork);
    public class NetworkGameState
    {
        //public static string SignalRConnection = @"https://localhost:7187/ChessMove";
        public static string SignalRConnection = @"https://nphsk35c-7187.brs.devtunnels.ms/ChessMove";
        private static HubConnection connection;
        private static event MakeMoveEvent OnReceiveMove;

        public static async Task InitializeConnectionAsync()
        {
            connection = new HubConnectionBuilder().WithUrl(SignalRConnection).WithAutomaticReconnect()
                .Build();
            
            await connection.StartAsync();
        }

        public static async Task<HubConnection> InitializeConnectionAsync(MakeMoveEvent makeMoveEvent)
        {
            await InitializeConnectionAsync();
            ReceiveMoveSubscribe(makeMoveEvent);
            StartListeners();
           
            return connection;
        }

        public static void ReceiveMoveSubscribe(MakeMoveEvent moveEvent) => OnReceiveMove += moveEvent;

        public static void StartListeners()
        {
            connection.On("OnReceiveMove", (string move) =>
            {
                OnReceiveMove?.Invoke(MoveData.Parse(move), true);
            });
        }

        public async static Task MakeMove(Move move)
        {
            await connection.InvokeAsync("OnMakeMove", move.ToJson());
        }
    }
}
