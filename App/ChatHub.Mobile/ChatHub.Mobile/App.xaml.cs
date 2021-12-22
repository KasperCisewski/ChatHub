using ChatHub.Mobile.Services;
using ChatHub.Mobile.Services.Implementation;
using ChatHub.Mobile.ViewModels;
using ChatHub.Mobile.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism.Ioc;
using Xamarin.Forms;

namespace ChatHub.Mobile
{
    public partial class App
    {
        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("NavigationPage/MainPage");

            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=d1b2986f-b62a-4e44-a2fd-ac822a2430bf;",
                typeof(Analytics), typeof(Crashes));
            
            base.OnStart();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatView, ChatViewModel>();

            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }
    }
}