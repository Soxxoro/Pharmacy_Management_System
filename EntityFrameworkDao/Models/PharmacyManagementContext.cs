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

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pharmacy_Management;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.Property(e => e.UnitIdFk).HasColumnName("Unit_Id_FK");

            entity.HasOne(d => d.UnitIdFkNavigation).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.UnitIdFk)
                .HasConstraintName("FK_Medicine_Unit");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitIdPk).HasName("PK__Unit__2C01E446C49A8152");

            entity.ToTable("Unit");

            entity.Property(e => e.UnitIdPk).HasColumnName("Unit_Id_PK");
            entity.Property(e => e.UnitName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Unit_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
