
using eBookSaleProject.Data;
using eBookSaleProject.Data.Services;
using eBookSaleProject.Data.Static;
using eBookSaleProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eBookSaleProject.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
          var allAuthors = await authorService.GetAllAsync();
          return View(allAuthors);
        
        }

        //Get:  Author/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        //Get:Author/Details/new{ id = author.id}
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await authorService.GetByIdAsync(id);
            if(authorDetails == null)
            {
                return View(nameof(NotFound));
            }
            return View(authorDetails);
        }

        //Get: Author/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var authorDetails = await authorService.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            
            return View(authorDetails);

        }

        //Get: Author/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await authorService.GetByIdAsync(id); 
            if(author==null)
            {
                return View(nameof(NotFound));
            }
            return View(author);
        }

        //Post: Author/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await authorService.GetByIdAsync(id);
            if(author == null)
            {
                return View(nameof(NotFound));
            }
            await authorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //Post: Author/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Biography")] Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await authorService.AddAsync(author);
            return RedirectToAction(nameof(Index));
        }

        //Post: Author/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Biography")] Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            
            await authorService.UpdateAsync(id, author);
            return RedirectToAction("Index");
        }

        
    }
}
