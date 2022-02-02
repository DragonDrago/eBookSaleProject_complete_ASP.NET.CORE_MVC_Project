using eBookSaleProject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
namespace eBookSaleProject.Models
{
    public class Author:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture Url")]
        [Required(ErrorMessage ="Profile Picture is Required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 100 chars")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is Required")]
        public string Biography { get; set; }

        // Relationships
        public List<Author_Book> Author_Books { get; set; }
    }
}
