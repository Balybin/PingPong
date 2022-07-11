using Microsoft.EntityFrameworkCore;
using Pong.Model;

namespace Pong.Services
{
    public class MessageRepository : IMessageRepository
    {
        public MessageRepository(AppDbContext db)
        {
            this._db = db;
        }

        private readonly AppDbContext _db;

        public async Task<MessageDto?> GetMessageById(int id)
        {
            return await _db.Messages.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesByUser(int user)
        {
            return await _db.Messages.Where(x => x.User == user).ToListAsync();
        }

        public async Task<MessageDto> AddMessage(MessageDto msg)
        {
                var res = await _db.Messages.AddAsync(msg);
                await _db.SaveChangesAsync();
                return res.Entity;
        }

        public async Task<MessageDto> DeleteMessage(MessageDto msg)
        {
            var res = _db.Messages.Remove(msg).Entity;
            await _db.SaveChangesAsync();
            return res;
        }

        public async Task<MessageDto> EditMessage(MessageDto msg)
        {
            var res = _db.Messages.Update(msg);
            await _db.SaveChangesAsync();
            return res.Entity;
        }
    }
}
