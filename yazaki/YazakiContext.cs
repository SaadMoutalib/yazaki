using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazaki
{
    class YazakiContext : DbContext
    {
        public DbSet<Formateur> formateurs { get; set; }
        public DbSet<Operateur> Operateurs { get; set; }
        public DbSet<Test> Tests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=yazakiDB;Integrated Security=True");
        }
    }
}
