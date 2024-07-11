using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;


namespace Domain.Contract.Services.Product.DomainEvent;
public static class DomainEvent
{
  
    public record ProductCreated(Guid EventId, Guid Id, string Name) : IDomainEvent, ICommand;
    public record ProductDeleted(Guid EventId, Guid Id) : IDomainEvent, ICommand;
    public record ProductUpdated(Guid EventId, Guid Id, string Name) : IDomainEvent, ICommand;
}
