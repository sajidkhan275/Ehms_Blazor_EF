using System.ComponentModel.DataAnnotations;

namespace EmployeeHealthMicroservice.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
