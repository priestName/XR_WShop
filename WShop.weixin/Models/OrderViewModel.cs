using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WShop.EFModel;

namespace WShop.weixin.Models
{
    public class OrderViewModel
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public Addrress Addrresses;
        public int ProNum;
        public decimal price;

        public IEnumerable<OrderBillFath> OrderBillFaths { get; set; }
        public int clas;
    }
}