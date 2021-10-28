using Prism.Commands;
using Prism.Mvvm;

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

        public MainPageViewModel()
        {
            OpenChat = new DelegateCommand(() =>
            {
                UserName = " TEst";
            });
        }
    }
}