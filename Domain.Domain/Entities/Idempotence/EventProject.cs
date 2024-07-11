using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Entities;

namespace Domain.Domain.Entities.Idempotence;
public class EventProject: Entity<Guid>
{
    public Guid EventId { get; set; }
    public string Type { get; set; } = string.Empty;

}
