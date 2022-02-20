using eBookSaleProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace eBookSaleProject.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
           base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author_Book>().HasKey(ab => new
            {
                ab.BookId,
                ab.AuthorId
            });

            modelBuilder.Entity<Author_Book>().HasOne(b => b.Book).WithMany(ab => ab.Author_Books).HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Author_Book>().HasOne(a => a.Author).WithMany(ab => ab.Author_Books).HasForeignKey(a => a.AuthorId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Author_Book> Author_Books { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        //Order related tables

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
    }
}
