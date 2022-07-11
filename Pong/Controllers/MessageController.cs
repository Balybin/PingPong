using Microsoft.AspNetCore.Mvc;
using Pong.Model;
using Pong.Services;

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
        public async Task<IEnumerable<MessageDto>> getByUserId(int id)
        {
            return await _msgService.GetMessagesByUser(id);
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> getById(int id)
        {
            var result = await _msgService.GetMessageById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<MessageDto>> editMessage([FromBody] MessageDto msg)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _msgService.EditMessage(msg);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<MessageDto>> deleteMessage(int id)
        {
            var result = await _msgService.DeleteMessage(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<MessageDto>> CreateMessage([FromBody] CreateMessageObject msg)
        {
            if (!ModelState.IsValid) return BadRequest();

            var res = await _msgService.AddMessage(new MessageDto { Message = msg.Message, User = msg.User });
            if (res == null) return NotFound();
            return Ok(res);

        }
    }
}
