using eBookSaleProject.Models;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using eBookSaleProject.Data.Base;

namespace eBookSaleProject.Data.Services
{
    public class AuthorService :EntityBaseRepository<Author>, IAuthorService
    {

        public AuthorService(AppDbContext appDbContext)
            :base(appDbContext)
        {
            
        }

       
    }
}
