using eBookSaleProject.Data.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
namespace eBookSaleProject.Models
{
    public class Publisher:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Logo")]
        public byte[] Logo { get; set; }

        [NotMapped]
        [Display(Name = "Logo")]
        public IFormFile LogoUpload { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        //Relationships
        public List<Book> Books { get; set; }
    }
}
