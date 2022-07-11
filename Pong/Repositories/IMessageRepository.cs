using Pong.Model;

namespace Pong.Services
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<MessageDto>> GetMessagesByUser(int user);
        public Task<MessageDto?> GetMessageById(int id);
        public Task<MessageDto?> AddMessage(MessageDto msg);
        public Task<MessageDto?> EditMessage(MessageDto msg);
        public Task<MessageDto?> DeleteMessage(int id);
    }
}
