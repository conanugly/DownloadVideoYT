using DownloadSolution.ViewModels.Utilities.Reponsitory;
using DownloadVideoSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSolution.Utilities.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(FindOptions? findOptions = null);
        Task<List<TEntity>> GetAllAsync(FindOptions? findOptions = null);
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? find = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity,bool>> predicate, FindOptions? find = null);
        void Add(TEntity entity);
        Task<int> AddAsync(TEntity entity);
        Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, FindOptions? find = null);
        void AddMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entities);
        void DeleteMany(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity,bool>> predicate);
    }
}
