using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatHub.Api.Controllers
{
    public class ChatTestController : Controller
    {
        private readonly IHubContext<Hub.ChatHub> _hubContext;
        // private readonly IConnectionManager _connectionManager;
        // public ChatTestController(IConnectionManager connectionManager)
        // {
        //     _connectionManager = connectionManager;
        //
        // }
        //

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
            await _hubContext.Clients.All.SendAsync("SendMessage", $"Home page loaded at: {DateTime.Now}", "test");

            return Ok("Sent message");
        }
    }
}