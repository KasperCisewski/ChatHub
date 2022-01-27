using System;
namespace ChatHub.Library.Models
{
    public class Message
    {
        public string Username { get; }
        public string Content { get; }
        public DateTime MessageSentTime { get; }
        
        public Message(string username, string content, DateTime messageSentTime)
        {
            Username = username;
            Content = content;
            MessageSentTime = messageSentTime;
        }
    }
}