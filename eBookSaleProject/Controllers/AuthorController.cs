
using eBookSaleProject.Data;
using eBookSaleProject.Data.Services;
using eBookSaleProject.Data.Static;
using eBookSaleProject.Models;
using eBookSaleProject.Models.PaginationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
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
        public async Task<IActionResult> Index(int pg = 1)
        {
          var allAuthors = await authorService.GetAllAsync();
            const int pageSize = 9;
            if (pg < 1)
            {
                pg = 1;
            }

            int rescCount = allAuthors.Count();

            var paginationModel = new PaginationModel(rescCount, pg, pageSize);

            int rescSkip = (pg - 1) * pageSize;

            var data = allAuthors.Skip(rescSkip).Take(paginationModel.PageSize).ToArray();
            this.ViewBag.PaginationModel = paginationModel;

            return View(data);
            //return View(allAuthors);

        }


        [AllowAnonymous]
        public async Task<FileResult> AuthorImage(int id)
        {

            Author author = await authorService.GetByIdAsync(id);
            if (author != null && author.Image?.Length > 0)
            {
                return File(author.Image, "image/jpeg", author.FullName + "-" + author.Id + ".jpg");
            }
            return null;
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
        public async Task<IActionResult> Create(Author author, IFormFile ImageUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            using(var stream =new MemoryStream())
            {
               await ImageUpload.CopyToAsync(stream);
                author.Image = stream.ToArray();
            }
            await authorService.AddAsync(author);
            return RedirectToAction(nameof(Index));
        }

        //Post: Author/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id,  Author author, IFormFile ImageUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            if(ImageUpload != null)
            {
                using (var stream = new MemoryStream())
                {
                    await ImageUpload.CopyToAsync(stream);
                    author.Image = stream.ToArray();
                }
            }
            await authorService.UpdateAsync(id, author);
            return RedirectToAction("Index");
        }

        
    }
}
