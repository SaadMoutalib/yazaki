using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<Formateur> Formateurs { get; }
        Repository<Operateur> Operateurs { get; }
        Repository<Test> Tests { get; }
    }
}
