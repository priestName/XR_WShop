using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WShop.IService;

namespace WShop.weixin.Controllers
{
    public class ShopCartController : Controller
    {
        public ICustomerService CustomerService { get; set; }
        // GET: ShopCart
        public ActionResult Index()
        {
            string OPID = Session["openid"].ToString();
            var shopcaer= CustomerService.GetEntities(b => b.OpenId== OPID);
            return View(shopcaer);
        }

    }
}