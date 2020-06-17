using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ZipPay.Models;

namespace ZipPay.Context
{
    public partial class ZipPayContext : DbContext
    {
        public ZipPayContext()
        {
        }

        public ZipPayContext(DbContextOptions<ZipPayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ZipPayAccount> ZipPayAccount { get; set; }
        public virtual DbSet<ZipPayUser> ZipPayUser { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ZipPayDB;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZipPayAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.Property(e => e.AccountName).HasMaxLength(50);

                entity.HasOne(d => d.ZipPay)
                    .WithMany(p => p.ZipPayAccount)
                    .HasForeignKey(d => d.ZipPayId)
                    .HasConstraintName("FK_ZipPayAccount_ZipPayUser");
            });

            modelBuilder.Entity<ZipPayUser>(entity =>
            {
                entity.HasKey(e => e.ZipPayId);

                entity.Property(e => e.Email).HasMaxLength(50);
            });
        }
    }
}
