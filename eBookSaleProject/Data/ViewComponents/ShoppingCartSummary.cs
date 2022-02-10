using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eBookSaleProject.Data.Cart;

namespace eBookSaleProject.Data.ViewComponents
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items = shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
