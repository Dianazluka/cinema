﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cinema_i_s.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cinema_inform_sistemEntities8 : DbContext
    {
        public cinema_inform_sistemEntities8()
            : base("name=cinema_inform_sistemEntities8")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<associate> associate { get; set; }
        public virtual DbSet<film> film { get; set; }
        public virtual DbSet<hall> hall { get; set; }
        public virtual DbSet<seance> seance { get; set; }
        public virtual DbSet<ticket> ticket { get; set; }
        public virtual DbSet<ticket_selling> ticket_selling { get; set; }
    }
}
