using System;
using System.Reactive;
using System.Reactive.Linq;
using ChatHub.Mobile.Services;
using ChatHub.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView
    {
        private readonly ChatViewModel _viewModel;
        private float _keyboardSize;

        public ChatView(ChatViewModel viewModel)
        {
            var keyboardInteractionService = DependencyService.Get<IKeyboardInteractionService>();
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
            keyboardInteractionService
                .KeyboardHeightChanged
                .Select(f =>
                {
                    _keyboardSize = f;
                    return Unit.Default;
                })
                .Subscribe();
        }

        private void MessageTextEntryHandle_Focused(object sender, FocusEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _viewModel.UserInputWasInvoked(e.IsFocused);

                if (e.IsFocused)
                {
                     contentView.HeightRequest = _keyboardSize;
                }
                else
                {
                    contentView.HeightRequest = 0;
                }
            });
        }
    }
}