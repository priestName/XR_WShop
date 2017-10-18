using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace WShop.IService
{
    public interface IBaseService<TEntity> where TEntity:class ,new ()
    {
        bool Add(TEntity TEntity);
        bool Remov(TEntity TEntity);
        bool Modify(TEntity TEntity);
        int GetCount(Func<TEntity, bool> whereLamebda);
        TEntity GetEntity(Func<TEntity,bool> whereLamebda);
        IEnumerable<TEntity> GetEntities(Func<TEntity, bool> whereLamebda);

        IEnumerable<TEntity> GetEntitiesByPpage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<TEntity, bool>> whereLamebda, Expression<Func<TEntity, TType>> orderByLamebda);
    }
}
