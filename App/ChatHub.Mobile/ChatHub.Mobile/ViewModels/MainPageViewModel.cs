using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ChatHub.Mobile.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public DelegateCommand OpenChat { get; }

        public MainPageViewModel(INavigationService navigationService)
        {
            OpenChat = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    return;
                }

                navigationService.NavigateAsync("ChatView", new NavigationParameters()
                {
                    {
                        "Username", UserName
                    }
                });
            });
        }
    }
}