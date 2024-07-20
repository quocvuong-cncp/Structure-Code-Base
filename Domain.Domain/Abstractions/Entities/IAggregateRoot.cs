using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;

namespace Domain.Domain.Abstractions.Entities;
public interface IAggregateRoot
{
     List<IDomainEvent> DomainEvents { set; get; }
     IReadOnlyCollection<IDomainEvent> GetDomainEvents();
    public void ClearDomainEvents();
    protected void RaiseDomainEvent(IDomainEvent domainEvent);
}
