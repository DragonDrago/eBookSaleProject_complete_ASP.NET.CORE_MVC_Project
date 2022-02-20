using Microsoft.AspNetCore.Mvc;
using eBookSaleProject.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eBookSaleProject.Data.Services;
using eBookSaleProject.Models;
using Microsoft.AspNetCore.Authorization;
using eBookSaleProject.Data.Static;
using Microsoft.AspNetCore.Http;
using System.IO;
using eBookSaleProject.Models.PaginationModel;

namespace eBookSaleProject.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    public class PublisherController : Controller
    {
        private readonly IPublisherService publisherService;

        public PublisherController( IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pg = 1)
        {
            var allPublishers = await publisherService.GetAllAsync();
            const int pageSize = 9;
            if (pg < 1)
            {
                pg = 1;
            }

            int rescCount = allPublishers.Count();

            var paginationModel = new PaginationModel(rescCount, pg, pageSize);

            int rescSkip = (pg - 1) * pageSize;

            var data = allPublishers.Skip(rescSkip).Take(paginationModel.PageSize).ToArray();
            this.ViewBag.PaginationModel = paginationModel;
           
            return View(data);
           // return View(allPublishers);
        }

        //Get: Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var publisherDetails = await publisherService.GetByIdAsync(id);
            if(publisherDetails == null)
            {
                return View(nameof(NotFound));
            }
            return View(publisherDetails);
        }

        //Get: Create

        public IActionResult Create()
        {
            return View();
        }

        //Post: Create
        [HttpPost]
        public async Task<IActionResult> Create( Publisher publisher, IFormFile LogoUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }
            using(var stream = new MemoryStream())
            {
               await LogoUpload.CopyToAsync(stream);
               publisher.Logo = stream.ToArray();
            }
            await publisherService.AddAsync(publisher);
            return RedirectToAction(nameof(Index));
        }



        //Get: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var publisherDetails = await publisherService.GetByIdAsync(id);
            if(publisherDetails == null)
            {
                return View(nameof(NotFound));
            }
            return View(publisherDetails);
        }


        [AllowAnonymous]
        public async Task<FileResult> LogoImage(int id)
        {

            Publisher publisher = await publisherService.GetByIdAsync(id);
            if (publisher != null && publisher.Logo?.Length > 0)
            {
                return File(publisher.Logo, "image/jpeg", publisher.Logo + "-" + publisher.Id + ".jpg");
            }
            return null;
        }


        //Post: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Publisher publisher, IFormFile LogoUpload)
        {

            if (!ModelState.IsValid)
            {
                return View(publisher);
            }
            using (var stream = new MemoryStream())
            {
                await LogoUpload.CopyToAsync(stream);
                publisher.Logo = stream.ToArray();
            }
            await publisherService.UpdateAsync(id, publisher);
            return RedirectToAction(nameof(Index));
        }

        //Get: Publisher/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await publisherService.GetByIdAsync(id);
            if (author == null)
            {
                return View(nameof(NotFound));
            }
            return View(author);
        }

        //Post: Publisher/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await publisherService.GetByIdAsync(id);
            if (author == null)
            {
                return View(nameof(NotFound));
            }
            await publisherService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
