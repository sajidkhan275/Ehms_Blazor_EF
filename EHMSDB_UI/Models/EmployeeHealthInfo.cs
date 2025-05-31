using System;
using System.Collections.Generic;

namespace EHMSDB_UI.Models;

public partial class EmployeeHealthInfo
{
    public int EmployeeHealthInfoId { get; set; }

    public int? EmpId { get; set; }

    public string? BloodGroup { get; set; }

    public bool? Disability { get; set; }

    public string? MedicalReportFileName { get; set; }

    public byte[]? MedicalReport { get; set; }

    public string? RecentMedicalReportPath { get; set; }
}
