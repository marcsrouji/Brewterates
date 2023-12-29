using Brewterates.Domain.Abstractions.IRepositories;
using Brewterates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Brewterates.Infrastructre.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected brewteratesDbContext _context;
        protected IDbContextTransaction _transaction;

        public RepositoryBase(brewteratesDbContext context) => _context = context;

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }

    }
}
