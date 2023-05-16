using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace TimeTracker.BL.Models;

public record UserListModel : ModelBase
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public required string? Photo { get; set; }

    public string FullName { get => $"{Name} {LastName}"; }

    public static UserListModel Empty => new()
    {
        ID = Guid.Empty,
        Name = string.Empty,
        LastName = string.Empty,
        Photo = null
    };
}
