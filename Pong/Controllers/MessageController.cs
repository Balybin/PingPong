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
        public MessageController(IMessageService msgService, ILogger<MessageController> logger)
        {
            this._msgService = msgService;
            this._logger = logger;
        }

        private readonly IMessageService _msgService;
        private readonly ILogger<MessageController> _logger;

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
            if (!ModelState.IsValid)
            {
                _logger.LogError("Cant create message" + JsonSerializer.Serialize(msg));
                return BadRequest();
            }

            var res = await _msgService.AddMessage(new MessageDto { Message = msg.Message, User = msg.User });
            if (res == null)
            {
                _logger.LogError("Cant create message" + JsonSerializer.Serialize(msg));
                return NotFound();
            }
            _logger.LogDebug("Message object was created:" + JsonSerializer.Serialize(res));
            return Ok(res);

        }
    }
}
