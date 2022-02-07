﻿using eBookSaleProject.Data.Base;
using eBookSaleProject.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;

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

        [Required(ErrorMessage = "Image url is required")]
        [Display(Name = "Image Url path")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Book File is required")]
        [Display(Name = "Book file Url path")]
        public string BookFileUrl { get; set; }

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
