using System;
using ChatHub.Mobile.Services;
using System.Reactive.Subjects;
using UIKit;

namespace ChatHub.Mobile.iOS.Services
{

    public class KeyboardInteractionService : IKeyboardInteractionService
    {
        private readonly Subject<float> _keyboardHeightChanged = new Subject<float>();

        public KeyboardInteractionService()
        {
            KeyboardHeightChanged = _keyboardHeightChanged;
            UIKeyboard.Notifications.ObserveWillShow((_, uiKeyboardEventArgs) =>
            {
                var newKeyboardHeight = (float)uiKeyboardEventArgs.FrameEnd.Height;
                _keyboardHeightChanged.OnNext(newKeyboardHeight);
            });

            UIKeyboard.Notifications.ObserveWillHide((_, uiKeyboardEventArgs) =>
            {
                _keyboardHeightChanged.OnNext(0);
            });
        }

        public IObservable<float> KeyboardHeightChanged { get; }
    }
}