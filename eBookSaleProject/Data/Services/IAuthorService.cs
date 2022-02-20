using eBookSaleProject.Models;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eBookSaleProject.Data.Base;

namespace eBookSaleProject.Data.Services
{
    public interface IAuthorService:IEntityBaseRepository<Author>
    {
    }
}
