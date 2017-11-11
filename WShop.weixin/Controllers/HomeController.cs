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
        public ActionResult Index(int i=1)
        {
            HomeViewModel homeViewModel=new HomeViewModel();
            homeViewModel.NoticeNum = NoticeService.GetCount(n => true);
            homeViewModel.Banners = BannerService.GetEntities(b => true);
            homeViewModel.Notices = NoticeService.GetEntitiesByPpage(3, 1, false, n => true, n => n.ModiTime);

            homeViewModel.Products = ProductService.GetEntitiesByPpage(3, 1, false, n=>n.Type==i, n => n.CreateTime);

            homeViewModel.user= Session["userinfo"] as OAuthUserInfo;
            //homeViewModel.caresum= ShopCartService.GetCount(n => n.CusId == 1);
            //Session["cartNum"] = homeViewModel.caresum;

            homeViewModel.tid = i;
            //addCus(homeViewModel.user);
            Session["openid"] = "oWY-Owxt2VJAiHNj23fdowUP0olE";
            Session["cusId"]= CustomerService.GetEntities(n => n.OpenId == Session["openid"].ToString()).First().ID;
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