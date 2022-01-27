using ChatHub.Library.Models;
namespace ChatHub.Mobile.Models
{
    public class MessageUIModel : Message
    {
        public bool IsMessageOwnerIsCurrentUser { get; }
        
        public MessageUIModel(Message message, bool isMessageOwnerIsCurrentUser) : base(message.Username, message.Content, message.MessageSentTime)
        {
            IsMessageOwnerIsCurrentUser = isMessageOwnerIsCurrentUser;
        }
    }
}