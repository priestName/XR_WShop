using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WShop.EFModel;
using WShop.IRepositoty;
using WShop.IService;

namespace WShop.Service
{
    class ShopCartService : BaseService<ShoppingCart>, IShopCartService
    {
        public ShopCartService(IShopCartRepositoty shopCartRepositoty) : base(shopCartRepositoty)
        {
        }
    }
}
