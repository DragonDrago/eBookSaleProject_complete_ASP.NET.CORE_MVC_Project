using Microsoft.AspNetCore.Mvc;
using eBookSaleProject.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eBookSaleProject.Data.Services;
using eBookSaleProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using eBookSaleProject.Data.Static;
using System.IO;
using Microsoft.AspNetCore.Http;
using eBookSaleProject.Models.PaginationModel;

namespace eBookSaleProject.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pg = 1)
        {
            var allBooks = await bookService.GetAllAsync(n => n.Publisher);

            const int pageSize = 9;
            if(pg < 1)
            {
                pg = 1;
            }

            int rescCount = allBooks.Count();

            var paginationModel = new PaginationModel(rescCount, pg, pageSize);

            int rescSkip = (pg - 1) * pageSize;

            var data = allBooks.Skip(rescSkip).Take(paginationModel.PageSize).ToArray();
            this.ViewBag.PaginationModel = paginationModel;
            //return View(allBooks);
            return View(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await bookService.GetAllAsync(n => n.Publisher);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = allBooks.Where(n=>n.Name.ToLower().Contains(searchString.ToLower())||
                n.Description.ToLower().Contains(searchString.ToLower() )
                ||n.BookCategory.ToString().ToLower().Contains(searchString.ToLower()));
                return View("Index",filterResult);
            }
            return View("Index",allBooks);
        }


        //Get: Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await bookService.GetBookByIdAsync(id);
            return View(bookDetails);
        }

        public async Task<IActionResult> Create()
        {
            var bookDropDownsData = await bookService.GetNewBookDropDownValues();
            ViewBag.AuthorsList = new SelectList(bookDropDownsData.Authors, "Id", "FullName");
            ViewBag.PublishersList = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
            return View();
        }

        //Post: Delete 

        public async Task<IActionResult> Delete(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if(book == null)
            {
                return View(nameof(NotFound));
            }
            await bookService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //Post: Create

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookViewModel, IFormFile ImageUpload,IFormFile BookFileUpload)
        {
            if (!ModelState.IsValid)
            {
                var bookDropDownsData = await bookService.GetNewBookDropDownValues();
                ViewBag.AuthorsList = new SelectList(bookDropDownsData.Authors, "Id", "FullName");
                ViewBag.PublishersList = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
                return View(bookViewModel);
            }

            using (var stream = new MemoryStream())
            {
                await ImageUpload.CopyToAsync(stream);
                bookViewModel.Image = stream.ToArray();
            }

            using(var stream = new MemoryStream())
            {
                await BookFileUpload.CopyToAsync(stream);
                bookViewModel.BookFile = stream.ToArray();
            }

            await bookService.AddNewBookAsync(bookViewModel);

            return RedirectToAction(nameof(Index));
        }

        //Get: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await bookService.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new BookViewModel()
            {
                Id = bookDetails.Id,
                Name = bookDetails.Name,
                Price = bookDetails.Price,
                Description = bookDetails.Description,
                Image = bookDetails.Image,
                BookFile = bookDetails.BookFile,
                EdititonDate = bookDetails.EdititonDate,
                BookCategory = bookDetails.BookCategory,
                PublisherId = bookDetails.PublisherId,
                AuthorIds = bookDetails.Author_Books.Select(n => n.AuthorId).ToList(),
            };

            var bookDropDownsData = await bookService.GetNewBookDropDownValues();
            ViewBag.AuthorsList = new SelectList(bookDropDownsData.Authors, "Id", "FullName");
            ViewBag.PublishersList = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
            return View(response);
        }


        [AllowAnonymous]
        public async Task<FileResult> BookImage(int id)
        {

            Book book = await bookService.GetBookByIdAsync(id);
            if (book != null && book.Image?.Length > 0)
            {
                return File(book.Image, "image/jpeg", book.Name + "-" + book.Id + ".jpg");
            }
            return null;
        }


        [AllowAnonymous]
        public async Task<FileResult> BookFile(int id)
        {

            Book book = await bookService.GetBookByIdAsync(id);
            if (book != null && book.BookFile?.Length > 0)
            {
                return File(book.BookFile, "application/pdf", book.BookFile + "-" + book.Id + ".pdf");
            }
            return null;
        }


        //Post: Edit

        [HttpPost]
        public async Task<IActionResult> Edit(int id,BookViewModel bookViewModel, IFormFile ImageUpload, IFormFile BookFileUpload)
        {
            if(id != bookViewModel.Id)
            {
                return View(nameof(NotFound));
            }
            if (!ModelState.IsValid)
            {
                var bookDropDownsData = await bookService.GetNewBookDropDownValues();
                ViewBag.AuthorsList = new SelectList(bookDropDownsData.Authors, "Id", "FullName");
                ViewBag.PublishersList = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
                return View(bookViewModel);
            }
            if (ImageUpload != null)
            {
                using (var stream = new MemoryStream())
                {
                    await ImageUpload.CopyToAsync(stream);
                    bookViewModel.Image = stream.ToArray();
                }
            }
            if(BookFileUpload != null)
            {
                using(var stream = new MemoryStream())
                {
                    await BookFileUpload.CopyToAsync(stream);
                    bookViewModel.BookFile = stream.ToArray();
                }
            }
            
            await bookService.UpdateBookAsync(bookViewModel);
            return RedirectToAction(nameof(Index));
        }

    }
}
