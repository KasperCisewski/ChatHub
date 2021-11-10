using System;
using System.Collections.ObjectModel;
using System.Threading;
using ChatHub.Library.Models;
using ChatHub.Mobile.Services;
using ChatHub.Mobile.Services.Implementation;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
namespace ChatHub.Mobile.ViewModels
{
    public class ChatViewModel : BindableBase, INavigationAware
    {
        private readonly IMessageService _messageService;
        private Command<string> sendMessageCommand;
        private CancellationTokenSource _cancellationTokenSource;
        
        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        
        public Command SendMessage => sendMessageCommand ??
            (sendMessageCommand = new Command<string>(async (message) => await _messageService.SendMessageAsync(new Message(_userName, message, DateTime.Now), _cancellationTokenSource)));

        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages => _messages;
        
        private string _messageText;

        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }

        
        public ChatViewModel()
        {
            _messageService = new MessageService();
            _messageService
                .MessageObservable
                .Subscribe(message =>
            {
                _messages.Add(message);
            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
        
        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            UserName = parameters["Username"].ToString();
            _cancellationTokenSource = new CancellationTokenSource();
            _messages = new ObservableCollection<Message>();
            await _messageService.InitializeConnection(_cancellationTokenSource);
        }
    }
}