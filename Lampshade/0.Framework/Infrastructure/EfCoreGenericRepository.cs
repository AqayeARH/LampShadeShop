using System.Linq.Expressions;
using _0.Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0.Framework.Infrastructure
{
    public class EfCoreGenericRepository<TKey, T> : IGenericRepository<TKey, T> where T : class
    {
        #region Constractor Injection

        private readonly DbContext _context;

        public EfCoreGenericRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        public T GetBy(TKey id)
        {
            return _context.Find<T>(id);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public bool IsExist(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
