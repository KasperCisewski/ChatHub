using Android.App;
using Android.Content.PM;
using Android.OS;
using ChatHub.Mobile.Android.Services;
using ChatHub.Mobile.Services;
using Xamarin.Forms;

namespace ChatHub.Mobile.Android
{
    [Activity(Label = "ChatHub.Mobile", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            DependencyService.Register<IKeyboardInteractionService, KeyboardInteractionService>();

            base.OnCreate(savedInstanceState);
            
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}