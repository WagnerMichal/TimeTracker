using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL.Entities
{
    public interface IEntity
    {
        Guid ID { get; set; }
    }
}
