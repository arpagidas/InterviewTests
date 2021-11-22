using BlazorApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace BlazorApp.Data
{
    public class BlazorAppContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public BlazorAppContext(DbContextOptions<BlazorAppContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>();
        }
    }
}
