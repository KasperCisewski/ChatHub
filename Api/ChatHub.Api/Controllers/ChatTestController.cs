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
        [Route("getMessages")]
        public ActionResult GetHubMessages()
        {
            return Ok("Get");
        }

        [HttpGet]
        [Route("sendMessage")]
        public async Task<ActionResult> SendMessage()
        {
            await _hubContext.Clients.All.SendAsync("SendMessageAsync", new Message("API", $"SEMD MESS ASYnc Home page loaded at: {DateTime.Now}", DateTime.Now));
            await _hubContext.Clients.All.SendAsync("SendMessage", new Message("API", $"SEMD MESS Home page loaded at: {DateTime.Now}", DateTime.Now));

            return Ok("Sent message");
        }
    }
}