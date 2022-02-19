using eBookSaleProject.Data.Base;
using eBookSaleProject.Data.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;

namespace eBookSaleProject.Models
{
    public class Book:IEntityBase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        [NotMapped]
        public IFormFile ImageUpload { get; set; }

        public byte[] Image { get; set; }

        [NotMapped]
        public IFormFile BookFileUpload { get; set; }

        public byte[] BookFile { get; set; }

        public DateTime EdititonDate { get; set; }

        public BookCategory BookCategory { get; set; }

        // Relationships
        public List<Author_Book> Author_Books { get; set; }

        //Publisher
        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
    }
}
