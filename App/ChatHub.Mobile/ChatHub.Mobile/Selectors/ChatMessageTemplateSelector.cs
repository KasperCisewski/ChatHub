using ChatHub.Mobile.Models;
using ChatHub.Mobile.Templates;
using Xamarin.Forms;
namespace ChatHub.Mobile.Selectors
{
    public class ChatMessageTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _incomingMessageTemplate = new ChatMessageIncomingTemplate();
        private readonly DataTemplate _userMessageTemplate = new ChatUserMessageTemplate();

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var chatModel = (MessageUIModel) item;

            return chatModel.IsMessageOwnerIsCurrentUser ? _userMessageTemplate : _incomingMessageTemplate;
        }
    }
}