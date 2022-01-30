using ChatHub.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView
    {
        private readonly ChatViewModel _viewModel;

        public ChatView(ChatViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void MessageTextEntryHandle_Focused(object sender, FocusEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _viewModel.UserInputWasInvoked(e.IsFocused);

                if (e.IsFocused)
                {
//TODO                    
                }

                var height = chatViewScrollView.Height;

                await chatViewScrollView.ScrollToAsync(0, height, true);
            });
        }
    }
}