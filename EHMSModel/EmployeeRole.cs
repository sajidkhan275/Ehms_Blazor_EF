
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHMSModel
{
    public class EmployeeRole
    {
        [Key]
        public int EmpRoleId { get; set; }

        [Required]
        public int EmpId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public string? EmployeeName { get; set; }

        public string? RolesId { get; set; }

        public string? Name { get; set; }
    }
}
