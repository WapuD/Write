using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WapuD.Data;

public partial class TradeContext : DbContext
{
    public TradeContext()
    {
    }

    public TradeContext(DbContextOptions<TradeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Orderinfo> Orderinfos { get; set; }

    public virtual DbSet<Orderproduct> Orderproducts { get; set; }

    public virtual DbSet<Pointsget> Pointsgets { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productcategory> Productcategories { get; set; }

    public virtual DbSet<Productdelivery> Productdeliveries { get; set; }

    public virtual DbSet<Productmanufacture> Productmanufactures { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;password=Qwerty123;database=trade", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Orderinfo>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orderinfo");

            entity.HasIndex(e => e.OrderPickupPoint, "Fkone_idx");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderFio)
                .HasMaxLength(100)
                .HasColumnName("OrderFIO");

            entity.HasOne(d => d.OrderPickupPointNavigation).WithMany(p => p.Orderinfos)
                .HasForeignKey(d => d.OrderPickupPoint)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fkone");
        });

        modelBuilder.Entity<Orderproduct>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductArticleNumber })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("orderproduct");

            entity.HasIndex(e => e.ProductArticleNumber, "ProductArticleNumber");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductArticleNumber)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ProductCount).HasColumnName("productCount");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderproduct_ibfk_1");

            entity.HasOne(d => d.ProductArticleNumberNavigation).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.ProductArticleNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderproduct_ibfk_2");
        });

        modelBuilder.Entity<Pointsget>(entity =>
        {
            entity.HasKey(e => e.PointsGetId).HasName("PRIMARY");

            entity.ToTable("pointsget");

            entity.Property(e => e.PointsGetId).HasColumnName("PointsGetID");
            entity.Property(e => e.PointsGetCiti).HasMaxLength(100);
            entity.Property(e => e.PointsGetStreet).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductArticleNumber).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.ProductSupplier, "FKtwo_idx");

            entity.HasIndex(e => e.ProductCategory, "FKtwoone_idx");

            entity.HasIndex(e => e.ProductManufacturer, "FKtwotwo_idx");

            entity.Property(e => e.ProductArticleNumber)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ProductCost).HasPrecision(19, 2);
            entity.Property(e => e.ProductDescription).HasColumnType("text");
            entity.Property(e => e.ProductName).HasColumnType("text");
            entity.Property(e => e.ProductPhoto).HasMaxLength(100);
            entity.Property(e => e.ProductUnit).HasMaxLength(10);

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKtwoone");

            entity.HasOne(d => d.ProductManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKtwotwo");

            entity.HasOne(d => d.ProductSupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductSupplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKtwo");
        });

        modelBuilder.Entity<Productcategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("productcategory");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("categoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<Productdelivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PRIMARY");

            entity.ToTable("productdelivery");

            entity.Property(e => e.DeliveryId)
                .ValueGeneratedNever()
                .HasColumnName("deliveryID");
            entity.Property(e => e.DeliveryName)
                .HasMaxLength(100)
                .HasColumnName("deliveryName");
        });

        modelBuilder.Entity<Productmanufacture>(entity =>
        {
            entity.HasKey(e => e.ManufactureId).HasName("PRIMARY");

            entity.ToTable("productmanufacture");

            entity.Property(e => e.ManufactureId)
                .ValueGeneratedNever()
                .HasColumnName("manufactureID");
            entity.Property(e => e.ManufactureName)
                .HasMaxLength(100)
                .HasColumnName("manufactureName");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserRole, "UserRole");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserLogin).HasColumnType("text");
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasColumnType("text");
            entity.Property(e => e.UserPatronymic).HasMaxLength(100);
            entity.Property(e => e.UserSurname).HasMaxLength(100);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
