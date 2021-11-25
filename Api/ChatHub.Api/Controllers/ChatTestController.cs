using System;
using System.Threading.Tasks;
using ChatHub.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatHub.Api.Controllers
{
    [AllowAnonymous]
    [Route("ChatTest")]
    public class ChatTestController : Controller
    {
        private readonly IHubContext<Hub.ChatHub> _hubContext;

        public ChatTestController(IHubContext<Hub.ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("sendMessage/{message}")]
        public async Task<ActionResult> SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", new Message("API", $"Message {message} at: {DateTime.Now}", DateTime.Now));

            return Ok("Sent message");
        }
    }
}