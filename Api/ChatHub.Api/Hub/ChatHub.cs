using System;
using System.Threading;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatHub.Api.Hub
{
    public class ChatHub : Hub<IChatHub>
    {
        private int _connectionCount;
        
        public Task SendMessageAsync(Message message)
        {
            return Clients.All.ReceiveMessage(message);
        }

        public Task SendUserInterfaceInformation(bool isTyping)
        {
            return Clients.Others.ReceiveInformationAboutUserInteraction(isTyping);
        }

        public override async Task OnConnectedAsync()
        {
            Interlocked.Increment(ref _connectionCount);
            
            await Clients.All.ReceiveChatUserQuantity(_connectionCount);
            
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Interlocked.Decrement(ref _connectionCount);

            await Clients.All.ReceiveChatUserQuantity(_connectionCount);
            
            await base.OnDisconnectedAsync(exception);
        }
    }
}