using Pong.Model;
using Pong.Models;
using System.Text.Json;

namespace Pong.Services
{
    public class MessageService : IMessageService
    {
        public MessageService(IMessageRepository messageRepository, ILogger<MessageService> logger)
        {
            this._messageRepository = messageRepository;
            this._logger = logger;
        }
        private readonly ILogger<MessageService> _logger;


        private readonly IMessageRepository _messageRepository;
        public async Task<ResponseDto<IEnumerable<MessageDto>>> GetMessagesByUser(int user)
        {
            var data = await _messageRepository.GetMessagesByUser(user);
            if (data.Count() == 0)
            {
                var responseDto = new ResponseDto<IEnumerable<MessageDto>> { Obj = null, ErrorMessage = "Message with id " + user + " not found", StatusCode = 404 };
                _logger.LogError(JsonSerializer.Serialize(responseDto));
                return responseDto;
            }
            return new ResponseDto<IEnumerable<MessageDto>> { Obj = data, ErrorMessage = null, StatusCode = 201 };
        }
        public async Task<ResponseDto<MessageDto>> GetMessageById(int id)
        {
            var data = await _messageRepository.GetMessageById(id);
            if (data == null)
            {
                var responseDto = new ResponseDto<MessageDto> { Obj = null, ErrorMessage = "Message with id " + id + " not found", StatusCode = 404 };
                _logger.LogError(JsonSerializer.Serialize(responseDto));
                return responseDto;
            }
            return new ResponseDto<MessageDto> { Obj = data, ErrorMessage = null, StatusCode = 201 };
        }
        public async Task<ResponseDto<MessageDto>> AddMessage(MessageDto msg)
        {
            if (msg == null)
            {
                var responseDto = new ResponseDto<MessageDto> { Obj = msg, ErrorMessage = "Message can't be null", StatusCode = 400 };
                _logger.LogError(JsonSerializer.Serialize(responseDto));
                return responseDto;
            }
            var data = await _messageRepository.AddMessage(msg);
            return new ResponseDto<MessageDto> { Obj = data, ErrorMessage = null, StatusCode = 200 };
        }
        public async Task<ResponseDto<MessageDto>> EditMessage(MessageDto msg)
        {
            if (msg == null)
            {
                var responseDto = new ResponseDto<MessageDto> { Obj = msg, ErrorMessage = "Message can't be null", StatusCode = 400 };
                _logger.LogError(JsonSerializer.Serialize(responseDto));
                return responseDto;
            }
            var editableMessage = await _messageRepository.GetMessageById(msg.Id);
            if (editableMessage == null)
            {
                var responseDto = new ResponseDto<MessageDto> { Obj = msg, ErrorMessage = "Message with selected id not found", StatusCode = 404 };
                _logger.LogError(JsonSerializer.Serialize(responseDto));
                return responseDto;
            }
            editableMessage.Status = msg.Status;
            editableMessage.User = msg.User;
            editableMessage.Message = msg.Message;
            var data = await _messageRepository.EditMessage(editableMessage);
            return new ResponseDto<MessageDto> { Obj = data, ErrorMessage = null, StatusCode = 200 };
        }
        public async Task<ResponseDto<MessageDto>> DeleteMessage(int id)
        {
            var deletableMessage = await _messageRepository.GetMessageById(id);
            if (deletableMessage == null)
            {
                var responseDto = new ResponseDto<MessageDto> { Obj = null, ErrorMessage = "Message with id " + id + " not found", StatusCode = 404 };
                _logger.LogError(JsonSerializer.Serialize(responseDto));
                return responseDto;
            }
            var data = await _messageRepository.DeleteMessage(deletableMessage);
            return new ResponseDto<MessageDto> { Obj = data, ErrorMessage = null, StatusCode = 200 };
        }

    }
}