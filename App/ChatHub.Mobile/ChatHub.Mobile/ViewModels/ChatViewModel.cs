using System;
using System.Collections.ObjectModel;
using ChatHub.Library.Models;
using ChatHub.Mobile.Models;
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
        private Command _sendMessageCommand;

        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public Command SendMessageCommand => _sendMessageCommand ??
            (_sendMessageCommand = new Command(async () =>
            {
                await _messageService.SendMessageAsync(new Message(_userName, MessageText, DateTime.Now));
                MessageText = string.Empty;
            }));

        private ObservableCollection<MessageUIModel> _messages;
        public ObservableCollection<MessageUIModel> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        private string _messageText;

        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }

        public ChatViewModel()
        {
            _messages = new ObservableCollection<MessageUIModel>();
            _messageService = new MessageService();
            _messageService
                .MessageObservable
                .Subscribe(message =>
                {
                    Messages.Add(message);
                });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            UserName = parameters["Username"].ToString();
            await _messageService.InitializeConnection(UserName);
        }
    }
}