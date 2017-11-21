using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using WShop.EFModel;
using WShop.IService;
using WShop.weixin.Models;

namespace WShop.weixin.Controllers
{
    public class OrderController : Controller
    {
        public IProductService ProductService { get; set; }
        public IOrderService OrddeService { get; set; }
        public IShopCartService ShopCartService { get; set; }
        public IAddrressService AddrressService { get; set; }
        OrderViewModel orderViewModel =new OrderViewModel();
        // GET: Order
        public ActionResult Index(int num=0)
        {
            orderViewModel.ProNum = num;
            var pro = ShopCartService.GetEntities(n => n.checks == 1);
            orderViewModel.ShoppingCarts = ShopCartService.GetEntities(n => n.checks == 1);
            orderViewModel.Addrresses = AddrressService.GetEntity(n => n.State == 1);
            foreach (var ppp in pro)
            {
                orderViewModel.price += ppp.Qty*ppp.Product.SellPrice;
            }
            return View(orderViewModel);
        }

        public ActionResult OrderPay(string code)
        {
            var orders=OrddeService.GetEntity(n => n.Code == code);

            return View(orders);
        }

        public ActionResult OrderPayON(string code)
        {
            return View();
        }
        public ActionResult OrderMain()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            int cusid = Convert.ToInt32(Session["cusId"]);
            var ord= OrddeService.GetEntities(n => n.CusId == cusid);
            return View(ord);
        }
        public void addOrder()
        {
              var aa = "200";
            var pink = 0;
            var memo = Request["Memo"];
            if (memo == "")
           {
                memo = null;}
            var pro = ShopCartService.GetEntities(n => n.checks == 1);
            orderViewModel.Addrresses = AddrressService.GetEntity(n => n.State == 1);
            foreach (var ppp in pro)
            {
                orderViewModel.price += ppp.Qty * ppp.Product.SellPrice;
            }
            int cusid = Convert.ToInt32(Session["cusId"]);
            foreach (var od in pro)
            {
                if (od.Product.IsPinkage)
                {
                    pink = 10;
                    break;
                }
            }
            var code = cusid+DateTime.Now.Year.ToString().Substring(2,2) + DateTime.Now.Month+ DateTime.Now.Day+ DateTime.Now.Hour+ DateTime.Now.Minute+ DateTime.Now .Second+ pro.First().ProCode.Substring(12,2);
            var sql = $"exec addOrder '{code}',{cusid},'{"待付款"}',{orderViewModel.price},{pink},'{memo}'";
            var nums = pro.Count()*2 + 1;
            if (OrddeService.QueryBySql(sql)== nums)
            {
                aa = code;
            }
            Response.ContentType = "text/plain";
            Response.Write(aa);
            Response.End();
        }
        public void OrderUp()
        {
            var aa = 200;
            var code = Request["Code"];
            var PayId =Convert.ToInt32(Request["PayId"]);
            var order=OrddeService.GetEntity(n => n.Code == code);
            order.PayId = PayId;
            order.State = "已付款";
            order.PayTime=DateTime.Now;
            order.PostTime= DateTime.Now.AddMinutes(1);
            if (OrddeService.Add(order))
            {
                aa = 100;
            }
            Response.ContentType = "text/plain";
            Response.Write(aa);
            Response.End();
        }
    }
}