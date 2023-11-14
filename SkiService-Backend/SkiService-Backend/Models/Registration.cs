using System;
using System.Collections.Generic;

namespace SkiService_Backend.Models;

public partial class Registration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Tel { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Service { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime FinishDate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }
}
