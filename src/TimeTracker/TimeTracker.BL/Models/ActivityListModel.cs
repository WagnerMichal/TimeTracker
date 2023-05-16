using System;
using System.Collections.Generic;
using TimeTracker.Common.Enums;

namespace TimeTracker.BL.Models;

public record ActivityListModel : ModelBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required Types Type { get; set; }
    public TimeSpan Duration => End - Start;
    public required Guid UserID { get; set; }
    public required Guid ProjectID { get; set; }
    public static ActivityListModel Empty => new()
    {
        ID = Guid.NewGuid(),
        UserID = Guid.Empty,
        ProjectID = Guid.Empty,
        Start = DateTime.Now,
        End = DateTime.Now.AddHours(1),
        Type = default
    };
}
