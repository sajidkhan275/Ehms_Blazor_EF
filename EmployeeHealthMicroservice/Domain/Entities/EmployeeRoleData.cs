using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeHealthMicroservice.Domain.Entities
{
    public class EmployeeRoleData
    {
        [Key]
        public int EmpRoleId { get; set; }

        [Required]
        public int EmpId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public EmployeeDetailsData? Employee { get; set; }
        public Role? Role { get; set; }

        [NotMapped]
        public string? EmployeeName { get; set; }
        [NotMapped]
        public string? Name { get; set; }
        [NotMapped]
        public string? RolesId { get; set; }

    }
}
