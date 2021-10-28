using Prism.Mvvm;
using Prism.Navigation;
namespace ChatHub.Mobile.ViewModels
{
    public class ChatViewModel : BindableBase, INavigationAware
    {
        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        
        public ChatViewModel()
        {
                
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
        
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _userName = parameters["Username"].ToString();
        }
    }
}