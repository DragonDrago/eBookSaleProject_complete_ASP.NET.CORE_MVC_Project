using eBookSaleProject.Data.Base;
using eBookSaleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace eBookSaleProject.Data.Services
{
    public class PublisherService:EntityBaseRepository<Publisher>,IPublisherService
    {
        public PublisherService(AppDbContext appDbContext)
            :base(appDbContext)
        {

        }
    }
}
