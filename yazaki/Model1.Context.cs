﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace yazaki
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class yazakiDBEntities : DbContext
    {
        public yazakiDBEntities()
            : base("name=yazakiDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Formateurs> Formateurs { get; set; }
        public virtual DbSet<Operateurs> Operateurs { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<Coordonnees> Coordonnees { get; set; }
    }
}
