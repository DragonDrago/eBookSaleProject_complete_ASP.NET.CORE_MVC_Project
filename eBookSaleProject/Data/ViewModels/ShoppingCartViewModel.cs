using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using eBookSaleProject.Data.Cart;

namespace eBookSaleProject.Data.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
    }
}
