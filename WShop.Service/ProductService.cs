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
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IProductRepositoty productRepositoty) : base(productRepositoty)
        {
        }
    }
}
