using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDao.Models;

public partial class PharmacyManagementContext : DbContext
{
    public PharmacyManagementContext()
    {
    }

    public PharmacyManagementContext(DbContextOptions<PharmacyManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pharmacy_Management;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B08DCF4C9");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineIdPk).HasName("PK__Medicine__899B561F92B721EC");

            entity.ToTable("Medicine");

            entity.Property(e => e.MedicineIdPk).HasColumnName("Medicine_Id_PK");
            entity.Property(e => e.MedicineDosage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Medicine_Dosage");
            entity.Property(e => e.MedicineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Medicine_Name");
            entity.Property(e => e.MedicinePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Medicine_Price");

            entity.HasOne(d => d.Category).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Medicine_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
