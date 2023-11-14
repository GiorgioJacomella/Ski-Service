using System;
using System.Collections.Generic;

namespace SkiService_Backend.Models;

public partial class UserInfo
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}
