using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Note> Notes { get; set; }
    }
}