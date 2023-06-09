using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WapuD.Data.Models_DB;

namespace WapuD.Data;

public partial class ShopContext : DbContext
{
    public ShopContext()
    {
    }

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;password=Qwerty123;database=shop", ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.IdCar).HasName("PRIMARY");

            entity.ToTable("car");

            entity.HasIndex(e => e.Vin, "vin_UNIQUE").IsUnique();

            entity.Property(e => e.IdCar).HasColumnName("id_car");
            entity.Property(e => e.ColorCar)
                .HasMaxLength(45)
                .HasColumnName("color_car");
            entity.Property(e => e.ConditionCar)
                .HasMaxLength(45)
                .HasColumnName("condition_car");
            entity.Property(e => e.CostCar).HasColumnName("cost_car");
            entity.Property(e => e.EngineCar)
                .HasMaxLength(45)
                .HasColumnName("engine_car");
            entity.Property(e => e.GearCar)
                .HasMaxLength(45)
                .HasColumnName("gear_car");
            entity.Property(e => e.GearboxCar)
                .HasMaxLength(45)
                .HasColumnName("gearbox_car");
            entity.Property(e => e.NameCar)
                .HasMaxLength(45)
                .HasColumnName("name_car");
            entity.Property(e => e.OwnerCar)
                .HasMaxLength(45)
                .HasColumnName("owner_car");
            entity.Property(e => e.OwnersCar).HasColumnName("owners_car");
            entity.Property(e => e.RunCar).HasColumnName("run_car");
            entity.Property(e => e.StatusCar)
                .HasMaxLength(45)
                .HasColumnName("status_car");
            entity.Property(e => e.TaxCar).HasColumnName("tax_car");
            entity.Property(e => e.TypeCar)
                .HasMaxLength(45)
                .HasColumnName("type_car");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .HasColumnName("vin");
            entity.Property(e => e.YearCar).HasColumnName("year_car");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.LoginUser, "login_user_UNIQUE").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LoginUser)
                .HasMaxLength(45)
                .HasColumnName("login_user");
            entity.Property(e => e.NameUser)
                .HasMaxLength(45)
                .HasColumnName("name_user");
            entity.Property(e => e.PasswordUser)
                .HasMaxLength(45)
                .HasColumnName("password_user");
            entity.Property(e => e.PatronymicUser)
                .HasMaxLength(45)
                .HasColumnName("patronymic_user");
            entity.Property(e => e.RoleUser)
                .HasDefaultValueSql("'1'")
                .HasColumnName("role_user");
            entity.Property(e => e.SurnameUser)
                .HasMaxLength(45)
                .HasColumnName("surname_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
