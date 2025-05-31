using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeHealthMicroservice.Domain.Entities
{
    public class EmployeeHealthInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeHealthInfoId { get; set; }
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter Blood Group")]
        public string? BloodGroup { get; set; }

        public string? MedicalReportFileName { get; set; }

        public string? RecentMedicalReportPath { get; set; }
        public bool? Disability { get; set; }

        public string? EmployeeName { get; set; }
    }
}
