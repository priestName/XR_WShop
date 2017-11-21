using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WShop.EFModel;
using WShop.IService;

namespace WShop.weixin.Controllers
{
    public class ShopCartController : Controller
    {
        public ICustomerService CustomerService { get; set; }
        public IShopCartService ShopCartService { get; set; }
        // GET: ShopCart
        public ActionResult Index()
        {
            int ID =Convert.ToInt32(Session["cusId"]);
            var shopcaer= CustomerService.GetEntities(b => b.ID== ID);
            var cus = CustomerService.GetEntity(n => n.ID == Convert.ToInt32(Session["cusId"]));
            Session["tel"] = cus.Phone;
            return View(shopcaer);
        }


        public void upCart()
        {
            int shu=0;
            string code = Request["codes"];
            string fu = Request["a"];
            int cusid = Convert.ToInt32(Session["cusId"]);
            if (code.Length == 13)
            {
                code = "0" + code;
            }
            var shopcart = ShopCartService.GetEntity(n => n.ProCode == code && n.CusId == cusid);
            if (fu=="+")
            {
                shopcart.Qty++;
                if (ShopCartService.Add(shopcart))
                {
                    shu = 1;
                }
            }
            else
            {
                shopcart.Qty--;
                if (ShopCartService.Add(shopcart))
                {
                    shu = 1;
                }
            }
            Response.ContentType = "text/plain";
            Response.Write(shu);
            Response.End();
        }

        public void CarOrder()
        {
            int aa = 0;
            string code = Request["codes"];
            var codes = code.Substring(0,code.Length-1).Split(';');
            int cusid = Convert.ToInt32(Session["cusId"]);
            foreach (var co in codes)
            {
                var shopcart = ShopCartService.GetEntity(n => n.ProCode == co && n.CusId == cusid);
                shopcart.checks = 1;
                if (ShopCartService.Add(shopcart))
                {
                    aa = 0;
                }
                else
                {
                    aa = 2000;
                    return;
                }
            }
            Response.ContentType = "text/plain";
            Response.Write(aa);
            Response.End();
        }

        public void delectCarts()
        {
            string aa = "删除失败";
            int cusid = Convert.ToInt32(Session["cusId"]);
            var code = Request["codes"];
            var codes = code.Split(';');
            var cc=0;
            foreach (var i in codes)
            {
                var cod=i;
                if (cod == "")
                {
                    break;
                }
                if (cod.Length == 13)
                {
                    cod = "0" + cod;
                }
                var shopcart = ShopCartService.GetEntity(n => n.ProCode == cod && n.CusId == cusid);
                if (ShopCartService.Remov(shopcart))
                {
                    cc ++;
                }
            }
            if (cc== codes.Length-1)
            {
                aa = "删除成功";
            }
            Response.ContentType = "text/plain";
            Response.Write(aa);
            Response.End();
        }
    }
}