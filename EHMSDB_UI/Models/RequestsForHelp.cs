using System;
using System.Collections.Generic;

namespace EHMSDB_UI.Models;

public partial class RequestsForHelp
{
    public int RequestForHelpId { get; set; }

    public int? EmpId { get; set; }

    public string? RequestDetails { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? RespondedAt { get; set; }

    public string? RespondedStatus { get; set; }

    public string? test { get; set; }

    public virtual Employee? Emp { get; set; }
}
