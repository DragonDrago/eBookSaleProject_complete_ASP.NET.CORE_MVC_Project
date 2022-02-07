using eBookSaleProject.Models;
using eBookSaleProject.Data.ViewModels;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eBookSaleProject.Data.Base;
namespace eBookSaleProject.Data.Services
{
    public interface IBookService:IEntityBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<BookDropDownViewModel> GetNewBookDropDownValues();
        Task AddNewBookAsync(BookViewModel bookViewModel);

        Task UpdateBookAsync(BookViewModel bookViewModel);
    }
}
