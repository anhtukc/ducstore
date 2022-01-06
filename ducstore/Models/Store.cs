using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ducstore.Models
{
    public partial class Store : DbContext
    {
        public Store()
            : base("name=Store")
        {
        }

        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<provider> providers { get; set; }
        public virtual DbSet<receiptbill> receiptbills { get; set; }
        public virtual DbSet<sellbill> sellbills { get; set; }
        public virtual DbSet<typeproduct> typeproducts { get; set; }
        public virtual DbSet<receiptbilldetail> receiptbilldetails { get; set; }
        public virtual DbSet<sellbilldetail> sellbilldetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.sellbills)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.identitycardnumber)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.receiptbills)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<news>()
                .Property(e => e.picture)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.productid)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.typeproductid)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.receiptbilldetails)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.sellbilldetails)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<provider>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<provider>()
                .HasMany(e => e.receiptbills)
                .WithRequired(e => e.provider)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receiptbill>()
                .HasMany(e => e.receiptbilldetails)
                .WithRequired(e => e.receiptbill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sellbill>()
                .HasMany(e => e.sellbilldetails)
                .WithRequired(e => e.sellbill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<typeproduct>()
                .Property(e => e.typeproductid)
                .IsUnicode(false);

            modelBuilder.Entity<receiptbilldetail>()
                .Property(e => e.productid)
                .IsUnicode(false);

            modelBuilder.Entity<sellbilldetail>()
                .Property(e => e.productid)
                .IsUnicode(false);
        }
    }
}
