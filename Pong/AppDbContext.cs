using Microsoft.EntityFrameworkCore;
using Pong.Model;

namespace Pong
{
    public class AppDbContext : DbContext
    {
        public DbSet<MessageDto> Messages { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=messagedb;Trusted_Connection=True;");
        }
    }
}
