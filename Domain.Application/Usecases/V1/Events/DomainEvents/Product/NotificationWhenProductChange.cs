using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;

namespace Domain.Application.Usecases.V1.Events.DomainEvents.Product;
public class NotificationWhenProductChange : IDomainEventHandler<CreatedProductDomainEvent>
{
    public async Task Handle(CreatedProductDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(10000);
    }
}
public class NotificationWhenProductChange3 : IDomainEventHandler<CreatedProductDomainEvent>
{
    public async Task Handle(CreatedProductDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1000);
    }
}
