using System;
using System.Collections.Generic;

namespace SkiService_Backend.Models;
/// <summary>
/// Model für sessions der mitarbeiter
/// Wurde mit dem Database first konzept generiert
/// </summary>

public partial class UserSession
{
    public int Id { get; set; }

    public string? SessionKey { get; set; }

    public int? UserId { get; set; }

    public virtual UserInfo? User { get; set; }
}
