using eBookSaleProject.Models;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eBookSaleProject.Data.Base;
using Microsoft.EntityFrameworkCore;
using eBookSaleProject.Data.ViewModels;

namespace eBookSaleProject.Data.Services
{
    public class BookService : EntityBaseRepository<Book>, IBookService
    {
        private readonly AppDbContext appDbContext;

        public BookService(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddNewBookAsync(BookViewModel bookViewModel)
        {
            var newBook = new Book()
            {
                Name = bookViewModel.Name,
                Description = bookViewModel.Description,
                Price = bookViewModel.Price,
                Image = bookViewModel.Image,
                BookFile = bookViewModel.BookFile,
                EdititonDate = bookViewModel.EdititonDate,
                BookCategory = bookViewModel.BookCategory,
                PublisherId = bookViewModel.PublisherId
            };
           await appDbContext.Books.AddAsync(newBook);
           await  appDbContext.SaveChangesAsync();
            //Add BookAuthor
            foreach(var authorId in bookViewModel.AuthorIds)
            {
                var newAuthorBook = new Author_Book()
                {
                    BookId = newBook.Id,
                    AuthorId = authorId
                };
                await appDbContext.Author_Books.AddAsync(newAuthorBook);
            }
            await appDbContext.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails = await appDbContext.Books
                  .Include(p => p.Publisher)
                  .Include(a => a.Author_Books).ThenInclude(a => a.Author)
                  .FirstOrDefaultAsync(n => n.Id == id);
            return bookDetails;
        }

        public async Task<BookDropDownViewModel> GetNewBookDropDownValues()
        {
            var response = new BookDropDownViewModel()
            {
                Authors = await appDbContext.Authors.OrderBy(n => n.FullName).ToListAsync(),
                Publishers = await appDbContext.Publishers.OrderBy(n => n.Name).ToListAsync()
            };
           return response;   
        }

        public async Task UpdateBookAsync(BookViewModel bookViewModel)
        {
            var dbBook = await appDbContext.Books.FirstOrDefaultAsync(n => n.Id == bookViewModel.Id);
            if(dbBook != null)
            {
                dbBook.Name = bookViewModel.Name;
                dbBook.Description = bookViewModel.Description;
                dbBook.Price = bookViewModel.Price;

                if (bookViewModel.Image != null)
                {
                    dbBook.Image = bookViewModel.Image;
                }

                if (bookViewModel.BookFile != null)
                {
                    dbBook.BookFile = bookViewModel.BookFile;
                }

                dbBook.EdititonDate = bookViewModel.EdititonDate;
                dbBook.BookCategory = bookViewModel.BookCategory;
                dbBook.PublisherId = bookViewModel.PublisherId;
                await appDbContext.SaveChangesAsync();
            }

            //Romve Existing Authors
            var existingAuthorsDb = appDbContext.Author_Books.Where(n=>n.BookId == bookViewModel.Id).ToList();
             appDbContext.Author_Books.RemoveRange(existingAuthorsDb);
            await appDbContext.SaveChangesAsync();

            //Add Authors
            foreach (var authorId in bookViewModel.AuthorIds)
            {
                var newAuthorBook = new Author_Book()
                {
                    BookId = bookViewModel.Id,
                    AuthorId = authorId
                };
                await appDbContext.Author_Books.AddAsync(newAuthorBook);
            }
            await appDbContext.SaveChangesAsync();
        }
    }
}
