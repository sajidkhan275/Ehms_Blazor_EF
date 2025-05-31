using System;
using System.Collections.Generic;

namespace EHMSDB_UI.Models;

public partial class EmployeePhysicalFitness
{
    public int EmployeePhysicalFitnessId { get; set; }

    public int? EmpId { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public virtual Employee? Emp { get; set; }
}
