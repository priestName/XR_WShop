using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WShop.EFModel;
using WShop.IService;
using WShop.weixin.Models;

namespace WShop.weixin.Controllers
{
    public class SortController : Controller
    {
        public ISortService SortService { get; set; }
        // GET: Sort
        public ActionResult Index()
        {
            SortViewModel sortViewModel = new SortViewModel();
            sortViewModel.Sort1 = SortService.GetEntities(n => n.UpCode == "0");
            sortViewModel.Sort2= SortService.GetEntities(n => n.UpCode != "0");
            return View(sortViewModel);
        }
    }
}