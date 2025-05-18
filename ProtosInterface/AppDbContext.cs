using Microsoft.EntityFrameworkCore;
using ProtosInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ProtosInterface
{
    class AppDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLink> ProductLinks { get; set; }
        public DbSet<Operation_Type> OperationTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(250)
                      .HasColumnName("name");

                entity.Property(p => p.TypeId)
                      .HasColumnName("type_id");

                entity.Property(p => p.CoopStatusId)
                      .HasColumnName("coop_status_id");

                entity.Property(p => p.Description)
                      .HasMaxLength(4000)
                      .HasColumnName("description");
            });

            // Конфигурация ProductLink
            modelBuilder.Entity<ProductLink>(entity =>
            {
                entity.ToTable("Product_Link");

                // Составной первичный ключ
                entity.HasKey(pl => new { pl.ParentProductId, pl.IncludedProductId });

                // Настройка связи с родительским продуктом
                entity.HasOne(pl => pl.ParentProduct)
                      .WithMany(p => p.ChildLinks)
                      .HasForeignKey(pl => pl.ParentProductId)
                      .OnDelete(DeleteBehavior.Restrict); // Или Cascade по необходимости

                // Настройка связи с включаемым продуктом
                entity.HasOne(pl => pl.IncludedProduct)
                      .WithMany(p => p.ParentLinks)
                      .HasForeignKey(pl => pl.IncludedProductId);

                entity.Property(pl => pl.Amount)
                      .HasColumnName("amount");
            });

            modelBuilder.Entity<Operation_Type>(entity =>
            {
                entity.ToTable("Operation_Type");
                entity.HasKey(ot => ot.Id);

                entity.Property(ot => ot.Name)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("name");

                entity.Property(ot => ot.Description)
                      .HasMaxLength(4000)
                      .HasColumnName("description");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("Operation");
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Code)
                      .HasColumnName("code");

                entity.Property(o => o.TypeId)
                      .IsRequired()
                      .HasColumnName("type_id");

                entity.Property(o => o.ProductId)
                      .HasColumnName("product_id");

                entity.Property(o => o.CoopStatusId)
                      .HasColumnName("coop_status_id");

                entity.Property(o => o.Description)
                      .HasMaxLength(4000)
                      .HasColumnName("description");

                entity.HasOne(o => o.Product)
                      .WithMany()
                      .HasForeignKey(o => o.ProductId);

                entity.HasOne(o => o.OperationType)
                      .WithMany()
                      .HasForeignKey(o => o.TypeId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DBConnection connect = new DBConnection();
            optionsBuilder.UseSqlServer(connect.ConnectionString);
            
        }
    }
}
