using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WShop.EFModel;
using WShop.IRepositoty;

namespace WShop.Repositoty
{
    public class NoticeRepositoty:BaseRepositoty<Notice>,INoticeRepositoty
    {
    }
}
