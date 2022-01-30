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
        private const string _hubReceiveMethod = "ReceiveMessage";
        private const string _hubSendMessageMethod = "SendMessageAsync";
        private const string _hubReceiveChatUserQuantityMethod = "ReceiveChatUserQuantity";
        private const string _hubSendUserInterfaceInformationMethod = "SendUserInterfaceInformation";
        private const string _hubReceiveUserInterfaceInformationMethod = "ReceiveInformationAboutUserInteraction";
        
        private readonly IUserService _userService;

        private HubConnection _hubConnection;

        private readonly Subject<MessageUIModel> _messageSubject = new Subject<MessageUIModel>();
        private readonly Subject<bool> _someoneIsTypingSubject = new Subject<bool>();
        private readonly Subject<int> _userChatQuantityChangedSubject = new Subject<int>();
        
        public MessageService(IUserService userService)
        {
            _userService = userService;
            MessageObservable = _messageSubject;
            OtherUserTypingObservable = _someoneIsTypingSubject;
            UserQuantityChangedObservable = _userChatQuantityChangedSubject;
        }
        
        public IObservable<MessageUIModel> MessageObservable { get; }
        
        public IObservable<bool> OtherUserTypingObservable { get; }
        
        public IObservable<int> UserQuantityChangedObservable { get; }

        public async Task InitializeConnectionAsync(string currentUsername)
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
        
        public async Task CloseConnectionAsync()
        {
            if (_hubConnection != null)
            {
                _hubConnection.Remove(_hubReceiveMethod);
                await _hubConnection.DisposeAsync();
            }
        }
        
        public Task SendUserInterfaceInformation(bool isTyping)
        {
            return _hubConnection.SendAsync(_hubSendUserInterfaceInformationMethod, isTyping);
        }

        public Task SendMessageAsync(Message message)
        {
            return _hubConnection.SendAsync(_hubSendMessageMethod, message);
        }
        
        private void AddHubListeners()
        {
            _hubConnection.On(_hubReceiveMethod, (Message message) =>
            {
                _messageSubject.OnNext(new MessageUIModel(message, _userService.GetCurrentUsername() == message.Username));
            });

            _hubConnection.On(_hubReceiveUserInterfaceInformationMethod, (bool isTyping) =>
            {
                _someoneIsTypingSubject.OnNext(isTyping);
            });

            _hubConnection.On(_hubReceiveChatUserQuantityMethod, (int quantity) =>
            {
                _userChatQuantityChangedSubject.OnNext(quantity);
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
    }
}