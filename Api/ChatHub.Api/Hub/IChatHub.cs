using System.Threading.Tasks;
using ChatHub.Library.Models;
namespace ChatHub.Api.Hub
{
    public interface IChatHub
    {
        Task ReceiveMessage(Message message);
        Task ReceiveChatUserQuantity(int quantity);
        Task ReceiveInformationAboutUserInteraction(bool isTyping);
    }
}