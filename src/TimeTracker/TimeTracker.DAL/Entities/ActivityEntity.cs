using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Common.Enums;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Entities
{
    public record ActivityEntity : IEntity
    {
        // String predelany na DateTime
        public required DateTime Start { get; set; }

        public required DateTime End { get; set; }

        public required Types Type { get; set; }

        public required string Description { get; set; }

        public required Guid ID { get; set; }

        public required Guid UserID { get; set; }

        public virtual UserEntity? User { get; init; }

        public required Guid ProjectID { get; set; }

        public virtual ProjectEntity? Project { get; init; }
    }
}
