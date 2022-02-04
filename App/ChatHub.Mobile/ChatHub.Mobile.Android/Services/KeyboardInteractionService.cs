using System;
using ChatHub.Mobile.Android.Listeners;
using ChatHub.Mobile.Services;

namespace ChatHub.Mobile.Android.Services
{
    public class KeyboardInteractionService : IKeyboardInteractionService
    {
        public KeyboardInteractionService()
        {
            var globalLayoutListener = new GlobalLayoutListener();
            KeyboardHeightChanged = globalLayoutListener.KeyboardHeightChanged;
        }

        /// <inheritdoc cref="IKeyboardInteractionService.KeyboardHeightChanged" />
        public IObservable<float> KeyboardHeightChanged { get; }   }
}