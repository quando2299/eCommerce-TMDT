﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookBook.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BookEntity : DbContext
    {
        public BookEntity()
            : base("name=BookEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<bill_detail> bill_detail { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<discount> discounts { get; set; }
        public virtual DbSet<order_detail> order_detail { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<rating> ratings { get; set; }
        public virtual DbSet<status_bill> status_bill { get; set; }
        public virtual DbSet<type> types { get; set; }
        public virtual DbSet<user> users { get; set; }

        public System.Data.Entity.DbSet<BookBook.Models.OrderInfo> OrderInfoes { get; set; }

        public System.Data.Entity.DbSet<BookBook.Models.OrdersModel> OrdersModels { get; set; }

        public System.Data.Entity.DbSet<BookBook.Models.OrderDetailModel> OrderDetailModels { get; set; }
    }
}
