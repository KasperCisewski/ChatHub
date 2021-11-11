using System;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using ChatHub.Mobile.Models;
namespace ChatHub.Mobile.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync(Message message);
        Task InitializeConnection(string currentUsername);
        IObservable<MessageUIModel> MessageObservable { get; }
    }
}