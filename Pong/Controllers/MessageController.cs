using Microsoft.AspNetCore.Mvc;
using Pong.Model;
using Pong.Services;
using System.Text.Json;

namespace Pong.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        public MessageController(IMessageService msgService)
        {
            this._msgService = msgService;
        }

        private readonly IMessageService _msgService;

        [HttpGet("getByUserId/{id}")]
        public async Task<ActionResult> getByUserId(int id)
        {
            var result = await _msgService.GetMessagesByUser(id);
            return new ObjectResult(result);
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var result = await _msgService.GetMessageById(id);
            return new ObjectResult(result);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> editMessage([FromBody] MessageDto msg)
        {
            var result = await _msgService.EditMessage(msg);
            return new ObjectResult(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> deleteMessage(int id)
        {
            var result = await _msgService.DeleteMessage(id);
            return new ObjectResult(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateMessage([FromBody] CreateMessageObject msg)
        {
            var result = await _msgService.AddMessage(new MessageDto { Message = msg.Message, User = msg.User });
            return new ObjectResult(result);
        }
    }
}
