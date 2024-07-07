using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;
using Domain.Domain.Entities.DomainEntities;

namespace Domain.Application.Usecases.V1.Queries.Product;
public sealed class GetProductQuery: IQuery<int>
{
    public int Quantity { get; set; }   
}
