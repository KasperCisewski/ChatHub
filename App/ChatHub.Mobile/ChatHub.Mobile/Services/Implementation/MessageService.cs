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
        private readonly IUserService _userService;
        private const string _hubReceiveMethod = "ReceiveMessage";
        private const string _hubSendMessageMethod = "SendMessageAsync";
        private HubConnection _hubConnection;

        private readonly Subject<MessageUIModel> _messageSubject = new Subject<MessageUIModel>();

        public MessageService(IUserService userService)
        {
            this._userService = userService;
            MessageObservable = _messageSubject;
        }

        public async Task InitializeConnection(string currentUsername)
        {
            _userService.SaveCurrentUsername(currentUsername);
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

                AddHubListeners();
                
                _hubConnection.Reconnected += HubConnectionOnReconnected;
                _hubConnection.Reconnecting += HubConnectionOnReconnecting;
                _hubConnection.Closed += HubConnectionOnClosed; 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AddHubListeners()
        {
            _hubConnection.On(_hubReceiveMethod, (Message message) =>
            {
                _messageSubject.OnNext(new MessageUIModel(message, _userService.GetCurrentUsername() == message.Username));
            });
        }
        
        private Task HubConnectionOnReconnected(string arg)
        {
            return Task.CompletedTask;
        }
        
        private Task HubConnectionOnReconnecting(Exception arg)
        {
            AddHubListeners();
            return Task.CompletedTask;
        }
        
        private Task HubConnectionOnClosed(Exception arg)
        {
            _hubConnection.Remove(_hubReceiveMethod);
            return Task.CompletedTask;
        }
        
        public IObservable<MessageUIModel> MessageObservable { get; }

        public Task SendMessageAsync(Message message)
        {
            return _hubConnection.SendAsync(_hubSendMessageMethod, message);
        }
    }
}