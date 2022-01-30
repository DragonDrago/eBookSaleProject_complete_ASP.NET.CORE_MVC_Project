using eBookSaleProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eBookSaleProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext appDbContext;

        public AuthorController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var data = appDbContext.Authors.ToList();
            return View(data);
        }
    }
}
