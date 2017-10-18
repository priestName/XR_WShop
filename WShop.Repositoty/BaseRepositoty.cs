using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WShop.EFModel;
using WShop.IRepositoty;

namespace WShop.Repositoty
{
    public class BaseRepositoty<TEntity> : IBaseRepositoty<TEntity> where TEntity : class, new()
    {
        private WShopDB _dbContext=DbConexFactory.GetIntance();
        private DbSet<TEntity> _dbSet;

        public BaseRepositoty()
        {
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Delete(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbSet.Remove(tEntity);
        }
        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }
        public void Update(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            //更改实体状态为已修改
            _dbContext.Entry(tEntity).State = EntityState.Modified;
        }
        public void insert(TEntity tEntity)
        {
            _dbSet.Add(tEntity);
        }
        public TEntity QueryEntity(Func<TEntity, bool> whereLamebda)
        {
            return _dbSet.FirstOrDefault(whereLamebda);
        }
        public IEnumerable<TEntity> QueryEntities(Func<TEntity, bool> whereLamebda)
        {
            return _dbSet.Where(whereLamebda);
        }
        public IEnumerable<TEntity> GetEntitiesByuPage<TType>(int pageSize, int pageIndex, bool isAsc, Expression<Func<TEntity, bool>> whereLamebda, Expression<Func<TEntity, TType>> orderByLamebda)
        {
            //查询
            var result=_dbSet.Where(whereLamebda);
            //排序
            result = isAsc ? result.OrderBy(orderByLamebda) : result.OrderByDescending(orderByLamebda);
            //分页
            var offset = (pageIndex - 1) * pageSize;//开始项索引
            result.Skip(offset).Take(pageSize);
            return result;
        }

        public int QueryCount(Func<TEntity, bool> whereLamebda)
        {
            return _dbSet.Count(whereLamebda);
        }
    }
}
