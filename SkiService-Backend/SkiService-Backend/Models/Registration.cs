using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiService_Backend.Models;

/// <summary>
/// Model für die Regestrierung neuer Aufträge
/// Wurde mit dem Database first konzept generiert
/// </summary>
public partial class Registration
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    public string Tel { get; set; } = null!;

    [Required]
    public string Priority { get; set; } = null!;

    [Required]
    public string Service { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime FinishDate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }
}
