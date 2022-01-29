using Microsoft.AspNetCore.Mvc;
using eBookSaleProject.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eBookSaleProject.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbContext appDbContext;

        public ProducerController( AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var allPublishers = await appDbContext.Publishers.ToListAsync();
            return View();
        }
    }
}
