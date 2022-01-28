using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
namespace eBookSaleProject.Models
{
    public class Author_Book
    {
        public int BookId { get; set; }
        public Book Book { get; set; }


        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
