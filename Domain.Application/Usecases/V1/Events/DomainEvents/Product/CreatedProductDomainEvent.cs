using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;

namespace Domain.Application.Usecases.V1.Events.DomainEvents.Product;
public class CreatedProductDomainEvent : IDomainEvent
{
    public Guid Id { get; init; }
    public Guid EventId { get; init; }
    public string Name { set; get; }

    public static CreatedProductDomainEvent Create(Guid id, Guid eventId ,string name)
    {
        return new CreatedProductDomainEvent(id, eventId, name);
    }
    public CreatedProductDomainEvent(Guid id, Guid eventId, string name)
    {
        if (!name.Contains("n_")) throw new Exception();
        Id = id;
        Name = name;
        EventId = eventId;
    }
}
