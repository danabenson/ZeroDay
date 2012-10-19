using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroDay.DAL.Contexts;
using ZeroDay.DAL.Interfaces;

namespace ZeroDay.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T: class
    {
        private readonly NatGeoContext _context;

        public Repository(NatGeoContext context)
        {
            _context = context;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
