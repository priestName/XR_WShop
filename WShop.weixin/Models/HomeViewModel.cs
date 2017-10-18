using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WShop.EFModel;

namespace WShop.weixin.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<Notice> Notices { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int NoticeNum { get; set; }

    }
}