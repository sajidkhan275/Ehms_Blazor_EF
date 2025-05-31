using Microsoft.EntityFrameworkCore;
using RequestHelpMicroservices.RequestForHelp;

namespace RequestHelpMicroservices.Dcontext
{
    public class EmpReqContext : DbContext
    {
        public EmpReqContext(DbContextOptions<EmpReqContext> options) : base(options) { }
        public DbSet<RequestHelp> RequestForHelps { get; set; }
        public DbSet<EmployeeDetailsData> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         //   modelBuilder.Entity<EmployeeDetailsData>().ToTable(nameof(EmployeeDetailsData), t => t.ExcludeFromMigrations());

            modelBuilder.Entity<RequestHelp>(entity =>
            {
                entity.ToTable("RequestHelp");
                entity.HasKey(e => e.RequestForHelpId);
                entity.Property(e => e.EmpId).IsRequired();
                entity.Property(e => e.RequestDetails).HasMaxLength(1000).IsRequired(true).IsUnicode(false);
                entity.Property(e => e.Status).HasMaxLength(50).IsRequired(true).IsUnicode(false);
                entity.Property(e => e.CreatedAt).HasColumnType("datetime").IsRequired(true);
                entity.Property(e => e.RespondedAt).HasColumnType("datetime").IsRequired(false);
                entity.Property(e => e.RespondedStatus).HasMaxLength(1000).IsRequired(false).IsUnicode(false);
            });
        }
    }
}
