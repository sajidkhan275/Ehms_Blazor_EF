using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeHealthMicroservice.Domain.Entities
{
    [Table("EmployeePhysicalFitness")]
    public class EmployeePhysicalFitness
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeePhysicalFitnessId { get; set; }

        public int EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter Weight")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Enter Weight")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Please Enter Height")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Enter Height")]
        public double Height { get; set; }

        [NotMapped]
        public string? EmployeeName { get; set; }

    }
}
