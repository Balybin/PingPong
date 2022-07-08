using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pong.Model;
using Pong.Services;
using System.Data.Entity.Validation;

namespace Pong.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        public MessageController(IMessageService msgService)
        {
            this._msgSErvice = msgService;
        }

        private readonly IMessageService _msgSErvice;

        [HttpGet("getByUserId/{id}")]
        public async Task<IEnumerable<MessageDto>> getByUserId(int id)
        {
            return await _msgSErvice.GetMessagesByUser(id);
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> getById(int id)
        {
            var result = await _msgSErvice.GetMessageById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<MessageDto>> editMessage([FromBody] MessageDto msg)
        {
            var result = await _msgSErvice.EditMessage(msg);
            if( result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<MessageDto>> deleteMessage(int id)
        {
            var result = await _msgSErvice.DeleteMessage(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<MessageDto>> CreateMessage([FromBody] CreateMessageObject msg)
        {
            if (msg.Message == null) return BadRequest();
            try
            {
                var res = await _msgSErvice.AddMessage(new MessageDto { Message = msg.Message, User = msg.User });
                return Ok(res);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is DbUpdateException || ex is DbEntityValidationException)
                {
                    return BadRequest();
                }
                else throw ex;
            }
        }
    }
}
