using Microsoft.EntityFrameworkCore;
using StockNewsletter.Models;

namespace StockNewsletter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User-WatchList relationship
            modelBuilder.Entity<WatchList>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchLists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
} 