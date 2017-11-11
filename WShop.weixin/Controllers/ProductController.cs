﻿using System;
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
        public ISortService SortService { get; set; }
        public IShopCartService ShopCartService { get; set; }
        public ICustomerService CustomerService { get; set; }
        // GET: Product
        public ActionResult Index(string Code,int i)
        {
            IEnumerable<Product> Products;
            if (i==0)
            {
                Products = ProductService.GetEntities(n => n.Name.Contains(Code) || n.Intro.Contains(Code) || n.Detail.Contains(Code));
            }
            else
            {
                Products = ProductService.GetEntities(n => n.Sorts.First().Code == Code);
            }
            return View(Products);
        }

        public ActionResult ProductDet(string code)
        {
            var Products = ProductService.GetEntities(n => n.Code==code);
            return View(Products);
        }

        public void addProd()
        {
            string ts = "加入购物车失败";
            ShoppingCart shopcCart = new ShoppingCart();
            shopcCart.ProCode = Request["codes"];
            shopcCart.Qty = Convert.ToInt32(Request["num"]);
            shopcCart.CusId = Convert.ToInt32(Session["cusId"]);
            shopcCart.CreateTime=DateTime.Now;
            var shca = ShopCartService.GetEntities(n => n.CusId == shopcCart.CusId && n.ProCode == shopcCart.ProCode);
            if (shca.Count()>0)
            {
                shopcCart.Qty += shca.First().Qty;
                if (ShopCartService.Add(shopcCart))
                {
                    ts = "加入购物车成功";
                }
            }
            else if (ShopCartService.Add(shopcCart))
            {
                ts = "加入购物车成功";
            }
            Response.ContentType = "text/plain";
            Response.Write(ts);
            Response.End();
        }

        public void LikePro()
        {
            CusPod cuspods = new CusPod();
        }
    }
}