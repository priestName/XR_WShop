using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WShop.EFModel;
using WShop.IService;

namespace WShop.weixin.Controllers
{
    public class ProductController : Controller
    {
        public IProductService ProductService { get; set; }
        // GET: Product
        public ActionResult Index(string Code)
        {
            var Products = ProductService.GetEntities( n => n.Sorts.First().Code == Code);
            return View(Products);
        }

        public ActionResult ProductDet(string code)
        {
            var Products = ProductService.GetEntities(n => n.Code==code);
            return View(Products);
        }
    }
}