using Microsoft.EntityFrameworkCore;
using Pong.Model;
using System.Data.Entity.Validation;

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

        public async Task<MessageDto?> AddMessage(MessageDto msg)
        {
            try
            {
                var res = await _db.Messages.AddAsync(msg);
                await _db.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is DbUpdateException || ex is DbEntityValidationException)
            {
                return null;
            }
        }

        public async Task<MessageDto?> DeleteMessage(int id)
        {
            var item = await _db.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return null;
            _db.Messages.Remove(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<MessageDto?> EditMessage(MessageDto msg)
        {
            try
            {
                var item = await _db.Messages.FirstOrDefaultAsync(x => x.Id == msg.Id);
                if (item == null) return null;
                item.User = msg.User;
                item.Status = msg.Status;
                item.Message = msg.Message;
                await _db.SaveChangesAsync();
                return item;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is DbUpdateException || ex is DbEntityValidationException)
            {
                return null;
            }
        }
    }
}
