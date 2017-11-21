using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WShop.EFModel;
using WShop.IService;
using WShop.weixin.Models;

namespace WShop.weixin.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerService customerService { get; set; }
        public ICusPodService CusPodService { get; set; }
        public IFeedBackService FeedBackService { get; set; }
        public IAddrressService AddrressService { get; set; }
        // GET: Customer
        public ActionResult Index()
        {
            CusViewModel cusViewModel = new CusViewModel();
            //cusViewModel.user = Session["userinfo"] as OAuthUserInfo;

            cusViewModel.user = customerService.GetEntities(n => n.ID ==Convert.ToInt32(Session["cusId"]));
            Session["tel"] = cusViewModel.user.First().Phone;
            return View(cusViewModel);
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Back()
        {
            return View();
        }
        public ActionResult LikePro()
        {
            var CusPro= CusPodService.GetEntities(n => n.CusId == Convert.ToInt32(Session["cusId"]));
            return View(CusPro);
        }

        public ActionResult address()
        {
            var aa = Convert.ToInt32(Session["cusId"]);
            var addrre= AddrressService.GetEntities(n => n.CusID == aa);
            return View(addrre);
        }

        public ActionResult addadrs(int ID=0)
        {
            IEnumerable<Addrress> addrre;
            if (ID != 0)
            {
                addrre = AddrressService.GetEntities(n => n.ID == ID);
                Session["ars"] = 1;
            }
            else
            {
                addrre = AddrressService.GetEntities(n => true);
                Session["ars"] = 0;
            }
            return View(addrre);
        }

        //单个删除
        public void DelLike()
        {
            string code = Request["codes"];
            int cusid = Convert.ToInt32(Session["cusId"]);
            if (code.Length == 13)
            {
                code = "0" + code;
            }
            string aa = "取消收藏失败";
            var like = CusPodService.GetEntity(n => n.CusId == cusid && n.ProCode == code);
            if (CusPodService.Remov(like))
            {
                aa = "取消收藏成功";
            }
            Response.ContentType = "text/plain";
            Response.Write(aa);
            Response.End();
        }
        //多个删除
        public void DelLikes()
        {
            string aa = "取消收藏失败";
            string code = Request["codes"];
            int cusid = Convert.ToInt32(Session["cusId"]);
            var codes = code.Split(';');
            var cc = 0;
            foreach (var i in codes)
            {
                var cod = i;
                if (cod == "")
                {
                    break;
                }
                if (cod.Length == 13)
                {
                    cod = "0" + cod;
                }
                var like = CusPodService.GetEntity(n => n.CusId == cusid && n.ProCode == cod);
                if (CusPodService.Remov(like))
                {
                    cc++;
                }
            }
            if (cc == codes.Length - 1)
            {
                aa = "删除成功";
            }
            Response.ContentType = "text/plain";
            Response.Write(aa);
            Response.End();
        }

        public void addback()
        {
            string aa = "反馈提交失败";
            string tel = Request["tels"];
            string body = Request["texts"];
            FeedBack Fback=new FeedBack();
            Fback.FTel = tel;
            Fback.FText = body;

            if (FeedBackService.Add(Fback))
            {
                aa = "反馈提交成功";
            }
            Response.ContentType = "text/plain";
            Response.Write(aa);
            Response.End();
        }
    }
}