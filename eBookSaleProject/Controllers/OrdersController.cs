﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using eBookSaleProject.Data.Services;
using eBookSaleProject.Data.Cart;
using eBookSaleProject.Data.ViewModels;

namespace eBookSaleProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBookService bookService;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrdersService ordersService;

        public OrdersController(IBookService bookService, ShoppingCart shoppingCart,IOrdersService ordersService)
        {
            this.bookService = bookService;
            this.shoppingCart = shoppingCart;
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await ordersService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item =await bookService.GetBookByIdAsync(id);
            if(item != null)
            {
                shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await bookService.GetBookByIdAsync(id);
            if (item != null)
            {
                shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";
            await  ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await shoppingCart.ClearShoppingCartAsync(); 
            return View("OrderCompleted");
        }

    }
}
