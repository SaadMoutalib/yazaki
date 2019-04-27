using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    class Repository<T> : IRepository<T> where T : class
    {
        private YazakiContext Context;
        private readonly DbSet<T> Entities;

        public Repository(YazakiContext context)
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
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate).FirstOrDefault();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
