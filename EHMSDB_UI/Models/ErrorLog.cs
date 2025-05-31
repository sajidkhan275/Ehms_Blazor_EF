using System;
using System.Collections.Generic;

namespace EHMSDB_UI.Models;

public partial class ErrorLog
{
    public int ErrorLogId { get; set; }

    public string? ErrorMessage { get; set; }

    public int? ErrorSeverity { get; set; }

    public int? ErrorState { get; set; }

    public string? ErrorProcedure { get; set; }

    public int? ErrorLine { get; set; }

    public DateTime? ErrorDateTime { get; set; }
}
