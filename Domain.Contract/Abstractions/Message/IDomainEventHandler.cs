using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Domain.Contract.Abstractions.Message;
public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
{

}
