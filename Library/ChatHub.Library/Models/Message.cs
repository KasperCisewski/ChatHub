using System;
namespace ChatHub.Library.Models
{
    public class Message
    {
        public string Username { get; }
        public string MessageText { get; }
        public DateTime DateTime { get; }
        
        public Message(string username, string messageText, DateTime dateTime)
        {
            Username = username;
            MessageText = messageText;
            DateTime = dateTime;
        }
    }
}