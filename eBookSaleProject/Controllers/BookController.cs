using Microsoft.AspNetCore.Mvc;
using eBookSaleProject.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eBookSaleProject.Data.Services;
using eBookSaleProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eBookSaleProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await bookService.GetAllAsync(n => n.Publisher);
            return View(allBooks);
        }

        //Get: Details
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

        //Post: Create

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                var bookDropDownsData = await bookService.GetNewBookDropDownValues();
                ViewBag.AuthorsList = new SelectList(bookDropDownsData.Authors, "Id", "FullName");
                ViewBag.PublishersList = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
                return View(bookViewModel);
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
                ImageUrl = bookDetails.ImageUrl,
                EdititonDate = bookDetails.EdititonDate,
                BookFileUrl = bookDetails.BookFileUrl,
                BookCategory = bookDetails.BookCategory,
                PublisherId = bookDetails.PublisherId,
                AuthorIds = bookDetails.Author_Books.Select(n => n.AuthorId).ToList(),
            };
            var bookDropDownsData = await bookService.GetNewBookDropDownValues();
            ViewBag.AuthorsList = new SelectList(bookDropDownsData.Authors, "Id", "FullName");
            ViewBag.PublishersList = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
            return View(response);
        }


        //Post: Edit

        [HttpPost]
        public async Task<IActionResult> Edit(int id,BookViewModel bookViewModel)
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
            await bookService.UpdateBookAsync(bookViewModel);
            return RedirectToAction(nameof(Index));
        }

    }
}
