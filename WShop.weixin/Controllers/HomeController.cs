using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WShop.EFModel;
using WShop.IService;
using WShop.weixin.Models;

namespace WShop.weixin.Controllers
{
    public class HomeController : Controller
    {
        public IBannerService BannerService { get; set; }
        public INoticeService NoticeService { get; set; }
        public IProductService ProductService { get; set; }
        

        // GET: Home
        public ActionResult Index(int i=1)
        {
            HomeViewModel homeViewModel=new HomeViewModel();
            homeViewModel.NoticeNum = NoticeService.GetCount(n => true);
            homeViewModel.Banners = BannerService.GetEntities(b => true);
            homeViewModel.Notices = NoticeService.GetEntitiesByPpage(3, 1, false, n => true, n => n.ModiTime);

            homeViewModel.Products = ProductService.GetEntitiesByPpage(3, 1, false, n=>n.Type==i, n => n.CreateTime);
            return View(homeViewModel);
        }
    }
}