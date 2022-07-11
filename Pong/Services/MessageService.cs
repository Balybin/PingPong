using Pong.Model;

namespace Pong.Services
{
    public class MessageService : IMessageService
    {
        public MessageService(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }

        private readonly IMessageRepository _messageRepository;
        public async Task<IEnumerable<MessageDto>> GetMessagesByUser(int user)
        {
            return await _messageRepository.GetMessagesByUser(user);
        }
        public async Task<MessageDto?> GetMessageById(int id)
        {
            return await _messageRepository.GetMessageById(id);
        }
        public async Task<MessageDto?> AddMessage(MessageDto msg)
        {
            return await _messageRepository.AddMessage(msg);
        }
        public async Task<MessageDto?> EditMessage(MessageDto msg)
        {
            return await _messageRepository.EditMessage(msg);
        }
        public async Task<MessageDto?> DeleteMessage(int id)
        {
            return await DeleteMessage(id);
        }

    }
}