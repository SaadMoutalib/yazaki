﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly yazakiDBEntities _context;
        public UnitOfWork(yazakiDBEntities context)
        {
            _context = context;
            Formateurs = new Repository<Formateurs>(context);
            Operateurs = new Repository<Operateurs>(context);
            Tests = new Repository<Test>(context);
        }
        public Repository<Formateurs> Formateurs { get; private set; }

        public Repository<Operateurs> Operateurs { get; private set; }

        public Repository<Test> Tests { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
