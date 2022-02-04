using System;
using System.Reactive.Subjects;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Xamarin.Essentials;

namespace ChatHub.Mobile.Android.Listeners
{
    public class GlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private readonly Activity activity;

        public GlobalLayoutListener()
        {
            activity = Platform.CurrentActivity;

            if (activity?.Window?.DecorView?.ViewTreeObserver == null)
            {
                throw new InvalidOperationException($"{this.GetType().FullName}.Constructor: The {nameof(this.activity)} or a follow up variable is null!");
            }

            activity.Window.DecorView.ViewTreeObserver.AddOnGlobalLayoutListener(this);
        }

        public Subject<float> KeyboardHeightChanged { get; } = new Subject<float>();

        public void OnGlobalLayout()
        {
            if (!KeyboardHeightChanged.HasObservers)
            {
                return;
            }

            var screenSize = new Point();
            activity.WindowManager?.DefaultDisplay?.GetSize(screenSize);
            var screenHeight = screenSize.Y;

            var screenHeightWithoutKeyboard = new Rect();

            int keyboardHeight;
            keyboardHeight = screenHeight - screenHeightWithoutKeyboard.Bottom;

            var keyboardHeightInDip = keyboardHeight / Resources.System?.DisplayMetrics?.Density ?? 1;

            if (keyboardHeightInDip < 0.0f)
            {
                keyboardHeightInDip = 0.0f;
            }

            KeyboardHeightChanged.OnNext(keyboardHeightInDip);
        }
    }
}