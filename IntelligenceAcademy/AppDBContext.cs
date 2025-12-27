using IntelligenceAcademy.Model;
using Microsoft.EntityFrameworkCore;

namespace IntelligenceAcademy
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
