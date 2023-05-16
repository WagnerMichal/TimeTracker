using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ObservableCollection<UserInProjectListModel> Users { get; init; } = new();

    public static ProjectDetailModel Empty => new()
    {
        ID = Guid.Parse("11111111-481d-427a-a971-ae306aba8c95"),
        Name = string.Empty,
        Description = string.Empty,
    };
}
