using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Interface.Repositories;
using Domain.Domain.Abstractions.Interface.UnitofWorks;
using Domain.Domain.Entities.DomainEntities;
using Domain.Persistence.Repositories.Base;

namespace Domain.Persistence.Repositories;
public class ProductRepository: GenericRepository<Product, Guid>,IProductRepository
{
    public ProductRepository(ApplicationDBContext context): base(context)
    {

    }
}
