using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WShop.EFModel;
using WShop.IService;
using WShop.weixin.Filters;
using WShop.weixin.Models;

namespace WShop.weixin.Controllers
{
    //[OAuthFilters]
    public class HomeController : Controller
    {
        public IBannerService BannerService { get; set; }
        public INoticeService NoticeService { get; set; }
        public IProductService ProductService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public IShopCartService ShopCartService { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            HomeViewModel homeViewModel=new HomeViewModel();
            homeViewModel.NoticeNum = NoticeService.GetCount(n => true);
            homeViewModel.Banners = BannerService.GetEntities(b => true);
            homeViewModel.Notices = NoticeService.GetEntitiesByPpage(3, 1, false, n => true, n => n.ModiTime);

            homeViewModel.Prod1 = ProductService.GetEntitiesByPpage(3, 1, false, n => n.Type == 1, n => n.CreateTime);
            homeViewModel.Prod2 = ProductService.GetEntitiesByPpage(3, 1, false, n => n.Type == 2, n => n.CreateTime);
            homeViewModel.Prod3 = ProductService.GetEntitiesByPpage(3, 1, false, n => n.Type == 3, n => n.CreateTime);

            homeViewModel.user= Session["userinfo"] as OAuthUserInfo;
            //homeViewModel.caresum= ShopCartService.GetCount(n => n.CusId == 1);
            //Session["cartNum"] = homeViewModel.caresum;

            //homeViewModel.tid = i;
            //addCus(homeViewModel.user);
            Session["openid"] = "oWY-Owxt2VJAiHNj23fdowUP0olE";
            var cus = CustomerService.GetEntity(n => n.OpenId == Session["openid"].ToString());
            //cus.Phone = "";
           //CustomerService.Add(cus);
            Session["cusId"]= cus.ID;
            Session["tel"] = cus.Phone;
            return View(homeViewModel);
        }
        public ActionResult Seek()
        {
            return View();
        }

        public ActionResult Notice()
        {
            var notice= NoticeService.GetEntities(n => true);
            return View(notice);
        }
        public ActionResult NoticeMain(int ID)
        {
            var notice = NoticeService.GetEntities(n =>n.ID==ID);
            return View(notice);
        }


        public void addTel()
        {
            var ts = "100";
            var tel=Request["codes"];
            Customer cus = CustomerService.GetEntity(n => n.ID == Convert.ToInt32(Session["cusId"]));
            cus.Phone = tel;
            if (CustomerService.Add(cus))
            {
                ts = "200";
            }
            Response.ContentType = "text/plain";
            Response.Write(ts);
            Response.End();
        }

        public void Order()
        {
            var ts = 200;
            var ID = Request["ProdId"];
            if (ID.Length == 13)
            {
                ID = "0" + ID;
            }
            int cusid = Convert.ToInt32(Session["cusId"]);
            ShoppingCart scr=new ShoppingCart();
            scr.ProCode = ID;
            scr.Qty = 1;
            scr.CusId = cusid;
            scr.CreateTime = DateTime.Now;
            scr.checks = 1;
            if (ShopCartService.Add(scr))
            {
                ts = 100;
            }
            Response.ContentType = "text/plain";
            Response.Write(ts);
            Response.End();
        }

        //public void addCus(OAuthUserInfo cus)
            //{
            //    HomeViewModel homeViewModel = new HomeViewModel();
            //    Customer cust = new Customer();
            //    cust.OpenId = cus.openid;
            //    cust.UImg= cus.headimgurl;
            //    cust.Name = cus.nickname;
            //    cust.CreateTime = DateTime.Now;
            //    if (CustomerService.GetCount(n => n.OpenId == cus.openid) < 1)
            //    {
            //        CustomerService.Add(cust);
            //    }
            //    else if(CustomerService.GetCount(n => n.UImg == cus.headimgurl && n.Name == cus.nickname) < 1)
            //    {
            //        cust.ID = CustomerService.GetEntities(c => c.OpenId == cust.OpenId).First().ID;
            //        CustomerService.Add(cust);
            //    }
            //    Session["openid"] = cus.openid;
            //}
        }
}