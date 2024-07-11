using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Repositories;
using Domain.Domain.Entities.Idempotence;
using Domain.Persistence.Repositories.Base;

namespace Domain.Persistence.Repositories;
public class EventRepositorry: GenericRepository<EventProject, Guid>, IEventProjectRepository
{
    public EventRepositorry(ApplicationDBContext context) : base(context)
    {

    }
}
