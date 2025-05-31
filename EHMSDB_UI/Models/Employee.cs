using System;
using System.Collections.Generic;

namespace EHMSDB_UI.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string? EmployeeCode { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public string? JobTitle { get; set; }

    public string Email { get; set; } = null!;

    public string AzureEntraId { get; set; } = null!;

    public virtual ICollection<EmployeePhysicalFitness> EmployeePhysicalFitnesses { get; set; } = new List<EmployeePhysicalFitness>();

    public virtual ICollection<RequestsForHelp> RequestsForHelps { get; set; } = new List<RequestsForHelp>();
}
