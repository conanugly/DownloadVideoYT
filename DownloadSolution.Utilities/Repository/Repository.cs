using DownloadSolution.Data.EF;
using DownloadSolution.ViewModels.Utilities.Reponsitory;
using DownloadVideoSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSolution.Utilities.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SolutionDbContext _context;
        public Repository(SolutionDbContext context)
        {
            this._context = context;
        }
        #region CURD
        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void AddMany(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = Find(predicate);
            _context.Set<TEntity>().RemoveRange(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
        {
            return Get(findOptions);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? find = null)
        {
            return Get(find).Where(predicate);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? find = null)
        {
            return Get(find).FirstOrDefault(predicate)!;
        }
        #endregion

        #region CURD Async
        public virtual async Task<List<TEntity>> GetAllAsync(FindOptions? findOptions = null)
        {
            return await Get(findOptions).ToListAsync();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, FindOptions? find = null)
        {
            try
            {
                var res = await GetDbAsync(find);
                return res.Where(predicate);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entities)
        {
            _context.Set<TEntity>().Remove(entities);
            await _context.SaveChangesAsync();
        }

        #endregion
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }

        private DbSet<TEntity> Get(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            var entity = _context.Set<TEntity>();

            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
                entity.IgnoreAutoIncludes().AsNoTracking();
            else if (findOptions.IsIgnoreAutoIncludes)
                entity.IgnoreAutoIncludes();
            else if (findOptions.IsAsNoTracking)
                entity.AsNoTracking();

            return entity;
        }

        private async Task<DbSet<TEntity>> GetDbAsync(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            var entity = _context.Set<TEntity>();

            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
                entity.IgnoreAutoIncludes().AsNoTracking();
            else if (findOptions.IsIgnoreAutoIncludes)
                entity.IgnoreAutoIncludes();
            else if (findOptions.IsAsNoTracking)
                entity.AsNoTracking();

            return await Task.FromResult(entity);
        }
    }
}
