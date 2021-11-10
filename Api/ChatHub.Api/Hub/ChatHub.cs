using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ChatHub.Api.Hub
{
    public class ChatHub : Hub<IChatHub>
    {
        
        // public async Task SendMessage(string user, string message)
        // {
        //     await Clients.All.SendAsync("ReceiveMessage", user, message);
        // }
    }
}