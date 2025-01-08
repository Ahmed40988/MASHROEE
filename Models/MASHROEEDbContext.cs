using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MASHROEE.Models
{
    public class MASHROEEDbContext:IdentityDbContext<Applicationuser>
    {
  
        public MASHROEEDbContext(DbContextOptions<MASHROEEDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // استدعاء إعدادات الـ Identity الافتراضية

            modelBuilder.Entity<Product>()
                .HasOne(p => p.user)
                .WithMany()
                .HasForeignKey(p => p.userid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.categoryid)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
