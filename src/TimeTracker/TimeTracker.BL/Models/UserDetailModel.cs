using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Models;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public required string Photo { get; set; }
    public ObservableCollection<UserInProjectListModel>? Projects { get; init; }

    public ObservableCollection<ActivityListModel>? Activities { get; init; }

    public static UserDetailModel Empty => new()
    {
        ID = Guid.Parse("00000000-481d-427a-a971-ae306aba8c95"),
        Name = string.Empty,
        LastName = string.Empty,
        Photo = string.Empty
    };
}
