using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeHealthMicroservice.Domain.Entities
{
    [Table("Employees")]
    public class EmployeeDetailsData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? EmployeeCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string? EmployeeName { get; set; }

        public int? DepartmentId { get; set; } = 0;

        public string? JobTitle { get; set; } = string.Empty;


        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? AzureEntraId { get; set; }

        public ICollection<EmployeeRoleData>? EmployeeRoles { get; set; }

        [NotMapped]
        public int RoleId { get; set; }
        [NotMapped]
        public string? DepartmentName { get; set; }
    }
}
