using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Entities.DomainEntities;

namespace Domain.Domain.Abstractions.Interface.Repositories;
public interface IProductRepository: IGenericRepository<Product, Guid>
{
}
