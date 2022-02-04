using System;

namespace ChatHub.Mobile.Services
{
    public interface IKeyboardInteractionService
    {
        IObservable<float> KeyboardHeightChanged { get; }
    }
}