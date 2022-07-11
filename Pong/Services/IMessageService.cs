using Pong.Model;
using Pong.Models;

namespace Pong.Services
{
    public interface IMessageService
    {
        public Task<ResponseDto<IEnumerable<MessageDto>>> GetMessagesByUser(int user);
        public Task<ResponseDto<MessageDto>> GetMessageById(int id);
        public Task<ResponseDto<MessageDto>> AddMessage(MessageDto msg);
        public Task<ResponseDto<MessageDto>> EditMessage(MessageDto msg);
        public Task<ResponseDto<MessageDto>> DeleteMessage(int id);
    }
}
