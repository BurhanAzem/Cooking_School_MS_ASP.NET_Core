using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cooking_School_ASP.NET.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBContext _dBContext;
        private readonly DbSet<T> _db;

        public GenericRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
            _db = _dBContext.Set<T>();
        }
        public async Task Delete(int id)
        {
            var entity =  await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);  
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> qurey = _db;
            if (include != null)
            {
                qurey = include(qurey);
            }
            return await qurey.FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> qurey = _db;
            if (orderBy != null)
            {
                qurey = orderBy(qurey);
            }
            if (expression != null)
            {
                qurey = qurey.Where(expression);
            }
            if (include != null)
            {
                qurey = include(qurey);
            }
            return await qurey.AsNoTracking().ToListAsync();
        }

        public async Task<X.PagedList.IPagedList<T>> GetPagedList(RequestParam requsetParam, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> qurey = _db; 
            if (include != null)
            {
                qurey = include(qurey);
            }
            if (orderBy != null)
            {
                qurey = orderBy(qurey);
            }
            if (expression != null)
            {
                qurey = qurey.Where(expression);
            }
            return await qurey.AsNoTracking().ToPagedListAsync(requsetParam.PageNumber, requsetParam.PageSize);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);    
        }

        public void Update(T entity)
        {
            _db.Update(entity);
        }
    }
}
