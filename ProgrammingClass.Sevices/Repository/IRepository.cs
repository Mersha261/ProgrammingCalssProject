using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Sevices.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);

        Task<TEntity> GetById(string id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Add(TEntity entity);

        void Update(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveAll(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> predicate);   

    }
}
