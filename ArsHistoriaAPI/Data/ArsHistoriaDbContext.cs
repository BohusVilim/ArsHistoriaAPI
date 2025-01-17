using ArsHistoriaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArsHistoriaAPI.Data
{
    public class ArsHistoriaDbContext : DbContext
    {
        public ArsHistoriaDbContext(DbContextOptions<ArsHistoriaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Style> Styles { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
    }
}
