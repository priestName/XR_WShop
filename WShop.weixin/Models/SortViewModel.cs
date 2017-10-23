using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WShop.EFModel;

namespace WShop.weixin.Models
{
    public class SortViewModel
    {
        public IEnumerable<Sort> Sort1 { get; set; }
        public IEnumerable<Sort> Sort2 { get; set; }
    }
}