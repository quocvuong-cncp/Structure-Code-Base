using Domain.Application.Usecases.V1.Commands.Product;
using Domain.Application.Usecases.V1.Queries.Product;
using Domain.Application.Usecases.V1.Events.DomainEvents;
using Domain.Contract.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Application.Usecases.V1.Events.DomainEvents.Product;

namespace Domain.API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class TestMediaR : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMediator _mediator;
    private readonly IPublisher _publisher;
    public TestMediaR(ISender sender, IMediator mediator, IPublisher publisher)
    {
        _sender = sender;
        _mediator = mediator;
        _publisher = publisher;
    }
    [HttpGet]
    public async Task<IActionResult> GetProduct()
    {
        var result = await _mediator.Send(new GetProductQuery());
        await _publisher.Publish(new CreatedProductDomainEvent(new Guid(), "kkk")); 
        if (result.IsFailure)
            return HandlerFailure(result);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct()
    {
        var result = await _mediator.Send(CreateProductCommand.Create(5, "Cola"));
        if (result.IsFailure)
            return HandlerFailure(result);
        return Ok(result.Value);
    }
    private  IActionResult HandlerFailure(Result result) =>
    result switch
    {
        { IsSuccess: true } => throw new InvalidOperationException(),
        IValidationResult validationResult =>
            BadRequest(
                CreateProblemDetails(
                    "Validation Error", StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors)),
        _ =>
            BadRequest(
                CreateProblemDetails(
                    "Bab Request", StatusCodes.Status400BadRequest,
                    result.Error))
    };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

}
