using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;

namespace Domain.Domain.Abstractions.Entities;
public abstract class AggregateRootAuditableEntity<T>: AuditableEntity<T>, IAggregateRoot
{
    public List<IDomainEvent> DomainEvents { set; get; } = new();

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => DomainEvents.ToList();

    public void ClearDomainEvents() => DomainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent) =>
        DomainEvents.Add(domainEvent);


}
