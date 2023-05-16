using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models;

public record UserInProjectDetailModel : ModelBase
{
    public Guid ProjectID { get; set; }
    
    public required string? ProjectName { get; set; }
    
    public Guid UserID { get; set; }
    public required string? UserName { get; set; }
    public required string? UserLastName { get; set; }
    public required string? UserPhoto { get; set; }
    public required string? UserEmail { get; set; }

    public static UserInProjectDetailModel Empty => new()
    {
        ID = Guid.NewGuid(),
        ProjectID = Guid.Empty,
        ProjectName = string.Empty,
        UserID = Guid.Empty,
        UserName = string.Empty,
        UserLastName = string.Empty,
        UserEmail = string.Empty,
        UserPhoto = null
    };
}
