using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Data;
using MyShop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyShop.DataAccess.Implementation;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate=null, string[]? includes = null)
    {
       IQueryable<T> query=_dbSet;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if(includes is not null)
        {
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
        }
        return [..query];
    }
    public IEnumerable<T> GetAll(string[]? includes = null)
    {
        IQueryable<T> query = _dbSet;
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return [.. query];
    }
    public IQueryable<T> GetQueryable()
    {
        return _dbSet;
    }

    public T? GetById(int id) => _dbSet.Find(id);
    public T? Find(Expression<Func<T, bool>> predicate, string[]? includes = null)
    {
        IQueryable<T> query = _dbSet;
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
       return query.SingleOrDefault(predicate);
    }

   public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    public void Update(T entity)
    {
       _dbSet.Update(entity);
    }
    public void Add(T entity)
    {
       _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
       _dbSet.AddRange(entities);
    }


    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
       _dbSet.RemoveRange(entities);
    }
}
