using System;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using ChatHub.Mobile.Models;
using ChatHub.Mobile.Models.Enums;
namespace ChatHub.Mobile.Services
{
    public interface IMessageService
    {
        IObservable<MessageUIModel> MessageObservable { get; }
        IObservable<bool> OtherUserTypingObservable { get; }
        IObservable<int> UserQuantityChangedObservable { get; }
        IObservable<ChatState>  ChatIsLoadingObservable { get; }

        Task SendMessageAsync(Message message);
        Task InitializeConnectionAsync(string currentUsername);
        Task CloseConnectionAsync();
        Task SendUserInterfaceInformation(bool isTyping);
    }
}