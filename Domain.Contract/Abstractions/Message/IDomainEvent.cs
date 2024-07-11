using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MassTransit.Topology;

namespace Domain.Contract.Abstractions.Message;
[ExcludeFromTopology]
public interface IDomainEvent:INotification
{
    public Guid EventId { get; init; }
    public Guid Id { get; init; }
}
