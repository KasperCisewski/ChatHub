using System;
namespace ChatHub.Library.Models
{
    public class Message
    {
        public string Username { get; }
        public string Content { get; }
        public DateTime DateTime { get; }
        
        public Message(string username, string content, DateTime dateTime)
        {
            Username = username;
            Content = content;
            DateTime = dateTime;
        }
    }
}