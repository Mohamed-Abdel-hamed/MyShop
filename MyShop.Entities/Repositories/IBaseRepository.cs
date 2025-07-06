using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repositories;
public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate=null, string[]? includes = null);
    IEnumerable<T> GetAll( string[]? includes = null);
    IQueryable<T> GetQueryable();
    T? GetById(int id);
    T? Find(Expression<Func<T, bool>> predicate, string[]? includes = null);
    bool Any(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
