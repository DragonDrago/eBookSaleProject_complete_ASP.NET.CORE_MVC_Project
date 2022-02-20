using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using eBookSaleProject.Models;

namespace eBookSaleProject.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);

        Task<List<Book>> GetOrderedBooksByUserIdAndRoleAsync(string userId,string userRole);
    }
}
