﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZzaDashboard
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class ZzaEntities : DbContext
    {
        public ZzaEntities()
            : base("ZzaEntities")
        {

        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {// Table names match entity names by default (don't pluralize)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Globally disable the convention for cascading deletes
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Customer>()
                        .Property(c => c.Id) // Client must set the ID.
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderItemOption> OrderItemOptions { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }
    }
}
