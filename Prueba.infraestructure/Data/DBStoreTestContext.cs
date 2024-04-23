using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prueba.Core.Entities;

#nullable disable

namespace Prueba.infraestructure.Data
{
    public partial class DBStoreTestContext : DbContext
    {
        public DBStoreTestContext()
        {
        }

        public DBStoreTestContext(DbContextOptions<DBStoreTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_order_Users");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Products_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Products_Catalog");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UserStore)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
