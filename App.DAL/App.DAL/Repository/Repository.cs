using App.DAL.Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().SingleOrDefault(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public List<string> GetDistinct(Expression<Func<T, string>> col)
        {
            return _context.Set<T>().Select(col).Distinct().ToList();
        }

        public T Find(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().SingleOrDefault(criteria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Where(criteria).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, bool isDesc = false)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (orderBy != null)
                query = isDesc ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);

            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, bool isDesc = false)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (orderBy != null)
                query = isDesc ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public bool UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return true;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

        public async Task<long> MaxAsync(Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(await _context.Set<T>().MaxAsync(column));
        }

        public async Task<long> MaxAsync(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(await _context.Set<T>().Where(criteria).MaxAsync(column));
        }

        public long Max(Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(_context.Set<T>().Max(column));
        }

        public long Max(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(_context.Set<T>().Where(criteria).Max(column));
        }

        public bool IsExist(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Any(criteria);
        }

        public T Last(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy)
        {
            return _context.Set<T>().OrderByDescending(orderBy).FirstOrDefault(criteria);
        }
    }
}
