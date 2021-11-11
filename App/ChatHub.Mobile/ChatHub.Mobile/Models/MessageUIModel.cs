using System;
using ChatHub.Library.Models;
namespace ChatHub.Mobile.Models
{
    public class MessageUIModel : Message
    {
        public bool IsMessageOwnerIsCurrentUser { get; }

        public MessageUIModel(Message message, bool isMessageOwnerIsCurrentUser) : base(message.Username, message.MessageText, message.DateTime)
        {
            IsMessageOwnerIsCurrentUser = isMessageOwnerIsCurrentUser;
        }
    }
}