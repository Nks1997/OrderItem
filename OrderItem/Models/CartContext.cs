using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderItem.Models
{
    public class CartContext
    {
        public Cart cart = new Cart();
        //private int cartid = 1;
        public Cart AddItem(int menuid)
        {
            cart.Id = 1;
            cart.UserId = 1;
            cart.MenuItemId = menuid;
            cart.MenuItemName = "Chocolate Brownie";
            
            //cartid += 1;
            return cart;
        }
    }
}
