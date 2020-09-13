using Microsoft.EntityFrameworkCore;
using RetailProduct.Models;
using System;

namespace RetailProduct.Entities
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StockInvoice> StockInvoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Cement",
                    Description = "Cement Items",
                },
                new Category
                {
                    Id = 2,
                    Name = "Iron",
                    Description = "Iron Rods",
                },
                new Category
                {
                    Id = 3,
                    Name = "Nails",
                    Description = "Inches Nails Items",
                },
                  new Category
                  {
                      Id = 4,
                      Name = "Block",
                      Description = "Block Items",
                  }
            );
        }
    }
}
