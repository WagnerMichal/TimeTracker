using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Common.Enums;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required DateTime Start { get; set; }

    public required DateTime End { get; set; }

    public required Types Type { get; set; }

    public required string Description { get; set; }

    public TimeSpan Duration => End - Start;

    public required Guid UserID { get; set; }

    public required Guid ProjectID { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        ID = Guid.NewGuid(),
        UserID = Guid.Empty,
        ProjectID = Guid.Empty,
        Start = DateTime.Now,
        End = DateTime.Now.AddHours(1),
        Type = default,
        Description = string.Empty
    };

}
