using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WShop.EFModel;
using WShop.IService;
using WShop.IRepositoty;


namespace WShop.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private IBaseRepositoty<TEntity> _baseRepositoty;
        //private IBannerRepositoty bannerRepositoty;

        public BaseService(IBaseRepositoty<TEntity> baseRepositoty)
        {
            _baseRepositoty = baseRepositoty;
        }

        //public void BannerBaseService(IBannerRepositoty bannerRepositoty)
        //{
        //    this.bannerRepositoty = bannerRepositoty;
        //}

        public bool Add(TEntity tEntity)
        {
            _baseRepositoty.insert(tEntity);
            return _baseRepositoty.SaveChanges();
        }
        public bool Modify(TEntity tEntity)
        {
            _baseRepositoty.Update(tEntity);
            return _baseRepositoty.SaveChanges();
        }
        public bool Remov(TEntity tEntity)
        {
            _baseRepositoty.Delete(tEntity);
            return _baseRepositoty.SaveChanges();
        }
        public TEntity GetEntity(Func<TEntity, bool> whereLamebda)
        {
            return _baseRepositoty.QueryEntity(whereLamebda);
        }
        public IEnumerable<TEntity> GetEntities(Func<TEntity, bool> whereLamebda)
        {
            return _baseRepositoty.QueryEntities(whereLamebda);
        }
        public IEnumerable<TEntity> GetEntitiesByPpage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<TEntity, bool>> whereLamebda, Expression<Func<TEntity, TType>> orderByLamebda)
        {
            return _baseRepositoty.GetEntitiesByuPage(pageSize, pageIndex, isAsc, whereLamebda, orderByLamebda);
        }
        public int GetCount(Func<TEntity, bool> whereLamebda)
        {
            return _baseRepositoty.QueryCount(whereLamebda);
        }
        public int QueryBySql(string SqlText)
        {
             return _baseRepositoty.QueryBySql(SqlText);
        }
    }
}
