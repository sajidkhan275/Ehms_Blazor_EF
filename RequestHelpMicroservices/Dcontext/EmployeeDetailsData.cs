using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RequestHelpMicroservices.Dcontext
{
    [Table("Employees")]
    public class EmployeeDetailsData
    {
        [Key]
        public int EmpId { get; set; }
        public string? EmployeeCode { get; set; } = string.Empty;
        public string? EmployeeName { get; set; }

        public string? JobTitle { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? AzureEntraId { get; set; }

    }
}
