using System;
using System.Threading;
using System.Threading.Tasks;
using ChatHub.Library.Models;
namespace ChatHub.Mobile.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync(Message message, CancellationTokenSource cancellationTokenSource);
        Task InitializeConnection(CancellationTokenSource cancellationTokenSource);
        IObservable<Message> MessageObservable { get; }
    }
}