using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Interface.Repositories;
using Domain.Domain.Entities.Idempotence;

namespace Domain.Domain.Abstractions.Repositories;
public interface IEventProjectRepository:  IGenericRepository<EventProject,Guid>
{
}
