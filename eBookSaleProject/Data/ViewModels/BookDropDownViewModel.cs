using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using eBookSaleProject.Models;
namespace eBookSaleProject.Data.ViewModels
{
    public class BookDropDownViewModel
    {
        public BookDropDownViewModel()
        {
            Authors = new List<Author>();
            Publishers = new List<Publisher>();
        }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
    }
}
