using eBookSaleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBookSaleProject.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext appDbContext;

        public OrdersService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId,string userRole)
        {
            var orders = await appDbContext.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Book)
                .Include(n=>n.User).ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(x => x.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await appDbContext.Orders.AddAsync(order);
            await appDbContext.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    BookId = item.Book.Id,
                    OrderId = order.Id,
                    Price = item.Book.Price
                };
              await appDbContext.OrderItems.AddAsync(orderItem);
            }
            await appDbContext.SaveChangesAsync();
        }
    }
}
