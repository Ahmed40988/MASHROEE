﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MASHROEE.Models
{
    public class MASHROEEDbContext:IdentityDbContext<Applicationuser>
    {
        public MASHROEEDbContext()
        {
        }

        public MASHROEEDbContext(DbContextOptions<MASHROEEDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Cartitem> Cartitems { get; set; }
        public DbSet<Cart> Cart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

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
