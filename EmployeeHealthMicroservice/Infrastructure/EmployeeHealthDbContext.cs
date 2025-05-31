using EmployeeHealthMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHealthMicroservice.Infrastructure
{
    public class EmployeeHealthDbContext : DbContext
    {
        public EmployeeHealthDbContext(DbContextOptions<EmployeeHealthDbContext> options) : base(options) { }

        public DbSet<DepartmentDetails> Departments { get; set; }
        public DbSet<EmployeeDetailsData> Employee { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeRoleData> EmployeeRoles { get; set; }
        public DbSet<EmployeeHealthInfo> EmployeeHealthInfos { get; set; }    
        public DbSet<EmployeePhysicalFitness> EmpPhysicalFitnessCxt { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DepartmentDetails>()
              .Property(d => d.DepartmentName)
              .IsRequired()
              .HasMaxLength(100);

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);
                entity.Property(r => r.Name).HasMaxLength(100).IsRequired();

                // Seed initial data
                entity.HasData(
                    new Role { RoleId = 1, Name = "Admin" },
                    new Role { RoleId = 2, Name = "HR" },
                    new Role { RoleId = 3, Name = "Employee" }
                );
            });

            modelBuilder.Entity<EmployeeDetailsData>(entity =>
            {
                entity.HasKey(e => e.EmpId);
                entity.Property(e => e.EmployeeName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.EmployeeCode).HasMaxLength(100).IsRequired(false).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.AzureEntraId).HasMaxLength(100).IsRequired();
                entity.Property(e => e.DepartmentId).IsRequired(false).IsUnicode(false);
                entity.Property(e => e.JobTitle).IsRequired(false).IsUnicode(false);
                // entity.HasOne(e => e.Department).WithMany().HasForeignKey(e => e.DepartmentId)
                // .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmployeeRoleData>(entity =>
            {
                entity.HasKey(er => er.EmpRoleId);
                entity.HasOne(er => er.Employee)
                      .WithMany(e => e.EmployeeRoles)
                      .HasForeignKey(er => er.EmpId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(er => er.Role)
                      .WithMany()
                      .HasForeignKey(er => er.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EmployeeHealthInfo>(entity =>
            {
                entity.ToTable("EmployeeHealthInfo");
                entity.HasKey(e => e.EmployeeHealthInfoId);
                entity.Property(e => e.EmpId).IsRequired();
                entity.Property(e => e.BloodGroup).HasMaxLength(50).IsRequired(false).IsUnicode(false);
                entity.Property(e => e.Disability).IsRequired(false);
                entity.Property(e => e.MedicalReportFileName).HasMaxLength(255).IsRequired(false).IsUnicode(false);
                entity.Property(e => e.RecentMedicalReportPath).HasMaxLength(500).IsRequired(false).IsUnicode(false);
            });


            //Cascade - Deleting a Category will delete all related Products      
            //RestrictCannot delete a Category if there are still Products associated with it.\
            //SetNull -ets the foreign key in the dependent entity to NULL instead of deleting it.
            //NoAction -The database might throw an error if you attempt to delete a parent with related children.
            //ClientSetNull -Sets the foreign key to NULL in memory, but does not perform any operation on the database.
        }
    }
}
