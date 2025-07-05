using Microsoft.EntityFrameworkCore;
using DonationTrackingSystem.Domain.Entities;

namespace DonationTrackingSystem.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        // Diğer DbSet'ler buraya eklenecek

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Model yapılandırmaları buraya eklenecek
        }
    }
}