using Domain.Contract.Abstractions.Message;
using Domain.Contract.Services.Product.DomainEvent;
using Domain.Domain.Entities.Outbox;
using Domain.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Domain.Infrastructure.BackgroudJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDBContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint; // Maybe can use more Rebus library
    private readonly IBusControl _busControl;

    //public ProcessOutboxMessagesJob(ApplicationDBContext dbContext, IPublishEndpoint publishEndpoint, IBusControl busControl)
    //{
    //    _dbContext = dbContext;
    //    _publishEndpoint = publishEndpoint;
    //    _busControl = busControl;
    //    var token = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    //    _busControl.StartAsync(token.Token);
    //}
    public ProcessOutboxMessagesJob(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
        //_publishEndpoint = publishEndpoint;
        //_busControl = busControl;
        //var token = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        //_busControl.StartAsync(token.Token);
    }

    public async Task Execute(IJobExecutionContext context)
    {
        
        //List<OutboxMessage> messages = await _dbContext
        //    .Set<OutboxMessage>()
        //    .Where(m => m.ProcessedOnUtc == null)
        //    .OrderBy(m => m.OccurredOnUtc)
        //    .Take(20)
        //    .ToListAsync(context.CancellationToken);

        //foreach (OutboxMessage outboxMessage in messages)
        //{
        //    IDomainEvent? domainEvent = JsonConvert
        //        .DeserializeObject<IDomainEvent>(
        //            outboxMessage.Content,
        //            new JsonSerializerSettings
        //            {
        //                TypeNameHandling = TypeNameHandling.All
        //            });

        //    if (domainEvent is null)
        //        continue;
        //    var xx = domainEvent.GetType().Name;
        //    var yy = nameof(DomainEvent.ProductCreated);

        //    try
        //    {
        //        switch (domainEvent.GetType().Name)
        //        {
        //            case nameof(DomainEvent.ProductCreated):
        //                var productCreated = JsonConvert.DeserializeObject<DomainEvent.ProductCreated>(
        //                            outboxMessage.Content,
        //                            new JsonSerializerSettings
        //                            {
        //                                TypeNameHandling = TypeNameHandling.All
        //                            });
        //                await _publishEndpoint.Publish(productCreated, context.CancellationToken);
        //                break;

        //            case nameof(DomainEvent.ProductUpdated):
        //                var productUpdated = JsonConvert.DeserializeObject<DomainEvent.ProductUpdated>(
        //                            outboxMessage.Content,
        //                            new JsonSerializerSettings
        //                            {
        //                                TypeNameHandling = TypeNameHandling.All
        //                            });
        //                await _publishEndpoint.Publish(productUpdated, context.CancellationToken);
        //                break;

        //            case nameof(DomainEvent.ProductDeleted):
        //                var productDeleted = JsonConvert.DeserializeObject<DomainEvent.ProductDeleted>(
        //                            outboxMessage.Content,
        //                            new JsonSerializerSettings
        //                            {
        //                                TypeNameHandling = TypeNameHandling.All
        //                            });
        //                await _publishEndpoint.Publish(productDeleted, context.CancellationToken);
        //                break;
        //            default:
        //                break;
        //        }

        //        outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        //    }
        //    catch (Exception ex)
        //    {
        //        outboxMessage.Error = ex.Message;
        //    }
        //}

        await _dbContext.SaveChangesAsync();
    }
}
