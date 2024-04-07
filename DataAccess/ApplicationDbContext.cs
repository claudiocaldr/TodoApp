using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasData(new List<Note> { new Note { Id = -1, Description = "aaa", Title = "bbb"},
                new Note { Id = -2, Description = "bbb", Title = "ccc", },
                new Note { Id = -3, Description = "ccc", Title = "ddd"}});
        }
    }
}