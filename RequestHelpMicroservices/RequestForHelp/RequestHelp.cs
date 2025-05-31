using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestHelpMicroservices.RequestForHelp
{
    public class RequestHelp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestForHelpId { get; set; }
        public int EmpId { get; set; }

        [Required]
        [StringLength(1000)]
        public string? RequestDetails { get; set; }

        [Required]
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? RespondedAt { get; set; }
        public string? RespondedStatus { get; set; }

        [NotMapped]
        public string? EmployeeName { get; set; }
    }
}
