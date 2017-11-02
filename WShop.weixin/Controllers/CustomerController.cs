using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WShop.IService;
using WShop.weixin.Models;

namespace WShop.weixin.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerService customerService { get; set; }
        // GET: Customer
        public ActionResult Index()
        {
            CusViewModel cusViewModel = new CusViewModel();
            //cusViewModel.user = Session["userinfo"] as OAuthUserInfo;

            cusViewModel.user = customerService.GetEntities(n => n.ID ==Convert.ToInt32(Session["cusId"]));
            return View(cusViewModel);
        }
    }
}