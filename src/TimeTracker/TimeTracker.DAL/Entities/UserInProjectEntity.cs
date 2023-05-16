using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Entities;

public record UserInProjectEntity : IEntity
{
    public required Guid UserID { get; set; }
    public virtual UserEntity? User { get; init; }
    public required Guid ProjectID { get; set; }
    public virtual ProjectEntity? Project { get; init; }
    public required Guid ID { get; set; }
}


