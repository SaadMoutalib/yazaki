using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetBykey(object id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllObject(string children, string children2);
        IEnumerable<T> GetAllQuery(Expression<Func<T, bool>> predicate);
        T Find(Expression<Func<T, bool>> predicate);
    }
}
