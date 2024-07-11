using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Services.Product.DomainEvent;
using Domain.Domain.Abstractions.Interface.UnitofWorks;
using Domain.Infrastructure.Abstractions;
using MassTransit;
using MediatR;

namespace Domain.Infrastructure.Consumer;
public static class ProductConsumer
{
    public class ProductCreatedConsumer : Consumer<DomainEvent.ProductCreated>
    {
        public ProductCreatedConsumer(ISender sender, IUnitofWorkEF unitofWorkEF)
            : base(sender, unitofWorkEF)
        {
        }
    }

    public class ProductDeletedConsumer : Consumer<DomainEvent.ProductDeleted>
    {
        public ProductDeletedConsumer(ISender sender, IUnitofWorkEF unitofWorkEF)
            : base(sender, unitofWorkEF)
        {
        }
    }

    public class ProductUpdatedConsumer : Consumer<DomainEvent.ProductUpdated>
    {
        public ProductUpdatedConsumer(ISender sender, IUnitofWorkEF unitofWorkEF)
            : base(sender, unitofWorkEF)
        {
        }
    }
}
