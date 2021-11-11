using System.Threading.Tasks;
using ChatHub.Library.Models;
namespace ChatHub.Api.Hub
{
    public interface IChatHub
    {
        Task ReceiveMessage(Message message);
    }
}