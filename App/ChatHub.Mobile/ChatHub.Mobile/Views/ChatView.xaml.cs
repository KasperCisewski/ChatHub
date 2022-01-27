using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void MessageTextEntryHandle_Focused(object sender, FocusEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var height = ChatViewScrollView.Height;
                
                await ChatViewScrollView.ScrollToAsync(0, height, true);
            });        
        }
    }
}