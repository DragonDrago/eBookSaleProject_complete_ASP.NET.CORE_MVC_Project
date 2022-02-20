using eBookSaleProject.Data.Base;
using eBookSaleProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eBookSaleProject.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext AppDbContext { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Book book)
        {
            var shoppingCartItem = AppDbContext.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == book.Id
            && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1
                };
                AppDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            AppDbContext.SaveChanges();
        }

        public void RemoveItemFromCart(Book book)
        {
            var shoppingCartItem = AppDbContext.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == book.Id
           && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    AppDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            AppDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = AppDbContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
                .Include(n => n.Book).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = AppDbContext.ShoppingCartItems.
                Where(n => n.ShoppingCartId == ShoppingCartId).
                Select(n => n.Book.Price * n.Amount).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await AppDbContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            AppDbContext.ShoppingCartItems.RemoveRange(items);
            await AppDbContext.SaveChangesAsync();
        }


    }
}
