using eBookSaleProject.Data.Base;
using eBookSaleProject.Data.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eBookSaleProject.Attributes;
using System.Linq;
using System.Threading;
using System;

namespace eBookSaleProject.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Book Name is required")]
        [Display(Name ="Book Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Book Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Book Price is required")]
        [Display(Name = "Book Price in $")]
        public double Price { get; set; }


        [NotMapped]
        [Display(Name = "Image jpeg")]
        [AllowedExtensionAttributes(new string[] { ".jpg", ".png" })]
        public IFormFile ImageUpload { get; set; }


        public byte[] Image { get; set; }

        [NotMapped]
        [Display(Name = "Book File (.pdf) ")]
        [AllowedExtensionAttributes(new string[] { ".pdf" })]
        public IFormFile BookFileUpload { get; set; }

        public byte[] BookFile { get; set; }


        [Required(ErrorMessage = "Edition Date is required")]
        [Display(Name = "Book's editon date")]
        public DateTime EdititonDate { get; set; }

        [Required(ErrorMessage = "Book Category is required")]
        [Display(Name = "Book Category")]
        public BookCategory BookCategory { get; set; }

        [Required(ErrorMessage = "Authors/Author is required")]
        [Display(Name = "Select Author(s)")]
        public List<int> AuthorIds { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        [Display(Name = "Select Publisher")]
        public int PublisherId { get; set; }

    }
}
