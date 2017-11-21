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
        public ISortService SortService { get; set; }
        public IShopCartService ShopCartService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public ICusPodService CusPodService { get; set; }
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
            shopcCart.checks = 0;
            var shca = ShopCartService.GetEntities(n => n.CusId == shopcCart.CusId && n.ProCode == shopcCart.ProCode);
            
            if (shca.Count()>0)
            {
                if (shca.First().Qty + shopcCart.Qty >= 5)
                {
                    ts = "购物车内该物品数量已达到上限";
                    return;
                }
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

        public void addorder()
        {
            var ts = 0;
            var Num = Convert.ToInt32(Request["proNum"]);
            var Code = Request["proCode"].ToString();
            ShoppingCart shopcCart = new ShoppingCart();
            shopcCart.ProCode = Code;
            shopcCart.Qty = Num;
            shopcCart.CusId = Convert.ToInt32(Session["cusId"]);
            shopcCart.CreateTime = DateTime.Now;
            shopcCart.checks = 1;
            if (Num > ProductService.GetEntity(n => n.Code == Code).Stock.QTY)
            {
                ts = 2000;
            }
            var shca = ShopCartService.GetEntities(n => n.CusId == shopcCart.CusId && n.ProCode == shopcCart.ProCode);
            if (shca.Count() > 0)
            {
                if (ShopCartService.Add(shopcCart))
                {
                    ts = shca.First().Qty;
                }
                else
                {
                    ts = 2000;
                }
            }
            else if (ShopCartService.Add(shopcCart))
            {
                ts = 0;
            }
            else
            {
                ts = 2000;
            }
            Response.ContentType = "text/plain";
            Response.Write(ts);
            Response.End();

        }
        public void LikePro()
        {
            string ts = "";
            CusPod cuspods = new CusPod();
            cuspods.CusId =Convert.ToInt32(Session["cusId"]);
            cuspods.ProCode = Request["codes"];
            cuspods.CreateTime = DateTime.Now;
            if (CusPodService.GetCount(n=>n.CusId==cuspods.CusId && n.ProCode== cuspods.ProCode) >0)
            {
                ts = "取消收藏失败";
                if (CusPodService.Remov(CusPodService.GetEntity(n => n.CusId == cuspods.CusId && n.ProCode == cuspods.ProCode)))
                {
                    ts = "取消收藏成功";
                }
            }
            else
            {
                ts = "添加收藏失败";
                if (CusPodService.Add(cuspods)) 
                {
                    ts = "添加收藏成功";
                }
            }
            Response.ContentType = "text/plain";
            Response.Write(ts);
            Response.End();
        }
    }
}