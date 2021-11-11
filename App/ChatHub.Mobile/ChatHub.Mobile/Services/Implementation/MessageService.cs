using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using ChatHub.Mobile.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace ChatHub.Mobile.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private HubConnection _hubConnection;

        private readonly Subject<MessageUIModel> _messageSubject = new Subject<MessageUIModel>();

        public MessageService()
        {
            MessageObservable = _messageSubject;
        }

        public async Task InitializeConnection(string currentUsername)
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://chathubtests2021.azurewebsites.net/chatHub")
                    .ConfigureLogging(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Trace);
                    })
                    .WithAutomaticReconnect()
                    .Build();

                await _hubConnection.StartAsync();

                _hubConnection.On("ReceiveMessage", (Message message) =>
                {
                     _messageSubject.OnNext(new MessageUIModel(message, currentUsername == message.Username));
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public IObservable<MessageUIModel> MessageObservable { get; }

        public Task SendMessageAsync(Message message)
        {
            return _hubConnection.SendAsync("SendMessageAsync", message);
        }
    }
}