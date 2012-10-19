using System.Linq;

namespace ZeroDay.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        T GetById(int id);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> GetAll();
    }
}