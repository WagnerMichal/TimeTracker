using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required Guid ID { get; set; }

    public virtual ICollection<UserInProjectEntity> Users { get; init; } = new List<UserInProjectEntity>();

    public virtual ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
}
