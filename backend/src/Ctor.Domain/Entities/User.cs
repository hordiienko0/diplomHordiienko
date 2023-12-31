﻿using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }

    /// <summary>
    /// User will be asked to change autogenerated password on first login.
    /// </summary>
    public bool AskToChangeDefaultPassword { get; set; } = true;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }

    public virtual ICollection<ProjectNote> ProjectNote { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }

    public long? CompanyId { get; set; }
    public Company? Company { get; set; }

    public ICollection<Assignee> Assignees { get; set; }
}