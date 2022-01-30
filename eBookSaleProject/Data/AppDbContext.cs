using eBookSaleProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace eBookSaleProject.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
           base(options)
        {

        }

        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Author_Book> Author_Books { get; set; }
        public DbSet<Book> Books { get; set; }
    
    }
}
