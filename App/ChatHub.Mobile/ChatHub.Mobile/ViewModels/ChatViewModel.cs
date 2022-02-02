using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using ChatHub.Mobile.Models;
using ChatHub.Mobile.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
namespace ChatHub.Mobile.ViewModels
{
    public class ChatViewModel : BindableBase, INavigationAware
    {
        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        private readonly IMessageService _messageService;
        private IAsyncCommand _sendMessageCommand;

        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public IAsyncCommand SendMessageCommand => _sendMessageCommand ??
            (_sendMessageCommand = new AsyncCommand(async () =>
            {
                if (string.IsNullOrWhiteSpace(MessageText))
                {
                    return;
                }

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

        private int _userOnChatQuantity;

        public int UserOnChatQuantity
        {
            get => _userOnChatQuantity;
            set => SetProperty(ref _userOnChatQuantity, value);
        }

        private bool _shouldShowSomeoneIsTyping;

        public bool ShouldShowSomeoneIsTyping
        {
            get => _shouldShowSomeoneIsTyping;
            set => SetProperty(ref _shouldShowSomeoneIsTyping, value);
        }

        public ChatViewModel(IMessageService messageService)
        {
            _messages = new ObservableCollection<MessageUIModel>();

            _messageService = messageService;
            _subscriptions.Add(_messageService
                .MessageObservable
                .Subscribe(message =>
                {
                    Messages.Add(message);
                }));
            
            _subscriptions.Add(_messageService
                .OtherUserTypingObservable
                .Subscribe(isTyping =>
                {
                    ShouldShowSomeoneIsTyping = isTyping;
                }));
            
            _subscriptions.Add(_messageService
                .UserQuantityChangedObservable
                .Subscribe(quantity =>
                {
                    UserOnChatQuantity = quantity;
                }));
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {
            await _messageService.CloseConnectionAsync();
            _subscriptions.Clear();
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            UserName = parameters["Username"].ToString();
            await _messageService.InitializeConnectionAsync(UserName);
        }

        public Task UserInputWasInvoked(bool isTyping)
        {
            return _messageService.SendUserInterfaceInformation(isTyping);
        }
    }
}