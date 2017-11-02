using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WShop.EFModel;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace WShop.weixin.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<Notice> Notices { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int NoticeNum { get; set; }
        public int tid { get; set; }
        public int caresum { get; set; }
        public OAuthUserInfo user { get; set; }
    }
}