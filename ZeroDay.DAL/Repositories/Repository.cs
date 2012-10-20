using System.Data;
using System.Linq;
using ZeroDay.DAL.Contexts;
using ZeroDay.DAL.Interfaces;

namespace ZeroDay.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly NatGeoContext _context;

        public Repository(NatGeoContext context)
        {
            _context = context;
        }

        #region IRepository<T> Members

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        #endregion
    }
}