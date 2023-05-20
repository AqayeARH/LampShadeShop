using System.Linq.Expressions;

namespace _0.Framework.Domain
{
    public interface IGenericRepository<in TKey, T> where T : class
    {
        T GetBy(TKey id);
        List<T> GetAll();
        void Create(T entity);
        bool IsExist(Expression<Func<T, bool>> expression);
        void Save();
    }
}
