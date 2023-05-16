using System;

namespace TimeTracker.BL.Models;

public record ProjectListModel : ModelBase
{
    public required string Name { get; set; }

    public static ProjectListModel Empty => new()
    {
        ID = Guid.NewGuid(),
        Name = string.Empty,
    };
}
