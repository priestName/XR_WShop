using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WShop.EFModel;

namespace WShop.weixin.Models
{
    public class CusViewModel
    {
        //public OAuthUserInfo user { get; set; }
        public IEnumerable<Customer> user { get; set; }
    }
}