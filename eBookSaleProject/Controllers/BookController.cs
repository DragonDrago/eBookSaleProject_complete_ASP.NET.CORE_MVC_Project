using Microsoft.AspNetCore.Mvc;
using eBookSaleProject.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eBookSaleProject.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext appDbContext;

        public BookController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await appDbContext.Books.ToListAsync();
            return View(allBooks);
        }
    }
}
