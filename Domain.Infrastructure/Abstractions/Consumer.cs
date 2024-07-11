using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;
using Domain.Domain.Abstractions.Interface.UnitofWorks;
using Domain.Persistence.Repositories.UnitofWork;
using MassTransit;
using MediatR;

namespace Domain.Infrastructure.Abstractions;
public abstract class Consumer<TMessage> : IConsumer<TMessage>
    where TMessage : class, IDomainEvent
{
    private readonly ISender _sender;
    private readonly IUnitofWorkEF _unitofWorkEF;
    //private readonly IMongoRepository<EventProjection> _eventRepository;
    protected Consumer(ISender sender, IUnitofWorkEF unitofWorkEF)
    {
        _sender = sender;
        _unitofWorkEF = unitofWorkEF;
    }
    public async Task Consume(ConsumeContext<TMessage> context)
    {
        var message = context.Message;
        var eventProject = await _unitofWorkEF.EventProjectRepository.FindSingleAsync((p) => p.EventId == context.Message.EventId);
        if(eventProject is null)
        {
            //_sender.Send()...........
            await _unitofWorkEF.EventProjectRepository.AddAsync(new Domain.Entities.Idempotence.EventProject() { Id = Guid.NewGuid(), EventId = context.Message.EventId });
            return;
        }
    

    }
}
