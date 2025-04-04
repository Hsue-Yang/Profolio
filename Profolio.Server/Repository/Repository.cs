using Microsoft.EntityFrameworkCore;
using Profolio.Server.Models;
using Profolio.Server.Repository.Interfaces;
using System.Linq.Expressions;

namespace Profolio.Server.Repository
{
	public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ProfolioContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public Repository(ProfolioContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        /// <summary> Add or Update entity </summary>
        /// <param name="predicate"></param>
        /// <param name="updateAction">執行操作不返回值</param>
        /// <param name="createEntity">執行操作返回值</param>
        /// <returns></returns>
        public async Task AddOrUpdateAsync(Expression<Func<T, bool>> predicate, Action<T> updateAction, Func<T> createEntity)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(predicate);
            if (existingEntity != null)
            {
                _dbSet.Remove(existingEntity);
                var newEntity = createEntity();
                _dbSet.Add(newEntity);
            }
            else
            {
                var newEntity = createEntity();
                _dbSet.Add(newEntity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        // 找單筆
        public T GetById<TId>(TId id)
        {
            return _dbSet.Find(new object[] { id });
        }

        // TId泛型型別參數，代表id可以為任一型別
        public async Task<T> GetByIdAsync<TId>(TId id)
        {
            return await _dbSet.FindAsync(new object[] { id });
        }

        // 找第一筆回傳
        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            return _dbSet.SingleOrDefault(expression);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.SingleOrDefaultAsync(expression);
        }

        // 有沒有資料 回傳布林
        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        // 整張資料表
        public async Task<List<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        // order
        public async Task<List<T>> OrderByAsync<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            return await _dbSet.OrderBy(keySelector).ToListAsync();
        }

        //Test
        public async Task<List<T>> TakeAsync(int count)
        {
            return await _dbSet.Take(count).ToListAsync();
        }

        //Test
        public async Task<List<T>> OrderByDescendingAsync<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            return await _dbSet.OrderByDescending(keySelector).ToListAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public IQueryable<T> GetAllReadOnly()
        {
            return _dbSet.AsNoTracking();
        }
        public IQueryable<T> Query()
        {
            return _dbSet;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        // 取分頁
        public async Task<List<T>> ListAsyncPage(Expression<Func<T, bool>> expression, int page, int pageSize)
        {
            return await _dbSet.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}