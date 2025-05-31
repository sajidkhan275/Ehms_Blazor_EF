using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHMSModel
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
