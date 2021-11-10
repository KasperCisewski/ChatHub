using System;
using ChatHub.Mobile.Services;
using ChatHub.Mobile.Services.Implementation;
using ChatHub.Mobile.ViewModels;
using ChatHub.Mobile.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prism.Ioc;
using Xamarin.Forms;

namespace ChatHub.Mobile
{
    public partial class App
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            SetupServices();

            var result = await NavigationService.NavigateAsync("NavigationPage/MainPage");

            if(!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
        
        private void SetupServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IMessageService, MessageService>();
            
            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatView, ChatViewModel>();
        }
    }
}