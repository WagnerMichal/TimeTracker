using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Entities;

public record UserEntity : IEntity
{
    public required Guid ID { get; set; }

    public required string Name { get; set; }

    public required string LastName { get; set; }

    public string? Email { get; set; }

    public required string Photo { get; set; }

    public virtual ICollection<UserInProjectEntity> Projects { get; init; } = new List<UserInProjectEntity>();

    public virtual ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();

}
