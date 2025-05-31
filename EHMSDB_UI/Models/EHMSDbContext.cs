using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EHMSDB_UI.Models;

public partial class EHMSDbContext : DbContext
{
    public EHMSDbContext()
    {
    }

    public EHMSDbContext(DbContextOptions<EHMSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeHealthInfo> EmployeeHealthInfos { get; set; }

    public virtual DbSet<EmployeePhysicalFitness> EmployeePhysicalFitnesses { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<RequestsForHelp> RequestsForHelps { get; set; }

    public virtual DbSet<Role> Roles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED1C43F485");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB992589A724");

            entity.Property(e => e.AzureEntraId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeHealthInfo>(entity =>
        {
            entity.HasKey(e => e.EmployeeHealthInfoId).HasName("PK__Employee__5D6AC54B99D96712");

            entity.ToTable("EmployeeHealthInfo");

            entity.Property(e => e.BloodGroup)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MedicalReportFileName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RecentMedicalReportPath)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeePhysicalFitness>(entity =>
        {
            entity.HasKey(e => e.EmployeePhysicalFitnessId).HasName("PK__Employee__8C6EEED35D5DA6E1");

            entity.ToTable("EmployeePhysicalFitness");

            entity.HasOne(d => d.Emp).WithMany(p => p.EmployeePhysicalFitnesses)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__EmployeeP__Emplo__4316F928");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasKey(e => e.EmpRoleId);

            entity.ToTable("EmployeeRole");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorLogId).HasName("PK__ErrorLog__D65247C21796B8B7");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage).HasMaxLength(4000);
            entity.Property(e => e.ErrorProcedure).HasMaxLength(200);
        });

        modelBuilder.Entity<RequestsForHelp>(entity =>
        {
            entity.HasKey(e => e.RequestForHelpId).HasName("PK__Requests__7C444F31CAA607A9");

            entity.ToTable("RequestsForHelp");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestDetails)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.RespondedAt).HasColumnType("datetime");
            entity.Property(e => e.RespondedStatus)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.test)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Emp).WithMany(p => p.RequestsForHelps)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__RequestsF__Emplo__4D94879B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A55369F30");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
