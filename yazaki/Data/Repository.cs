using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext Context;
        private readonly DbSet<T> Entities;

        public Repository(DbContext context)
        {
            Context = context;
            Entities = Context.Set<T>();
        }
        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public T GetBykey(object id)
        {
            return Entities.Find(id);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }
        public IEnumerable<T> GetAllObject(string children)
        {
            return Entities.Include(children).ToList();
        }
        public IEnumerable<T> GetAllQuery(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate);
        }
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate).FirstOrDefault();
        }

    }
}
