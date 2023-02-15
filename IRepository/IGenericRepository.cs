using Cooking_School_ASP.NET.Dtos;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using X.PagedList;

namespace Cooking_School_ASP.NET.IRepository
{
    public interface IGenericRepository <T> where T : class
    {
        Task Insert(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<IPagedList<T>> GetPagedList(RequestParam requsetParam, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T> , IIncludableQueryable<T, object>> include = null);
        Task InsertRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);

    }
}
