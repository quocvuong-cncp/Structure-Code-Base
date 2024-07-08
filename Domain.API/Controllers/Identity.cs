using Domain.Application.Usecases.V1.Queries.Identity;
using Domain.Contract.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class Identity : ControllerBase
{
    private readonly ISender _sender;

    public Identity(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost]
    public async Task<IActionResult> Login(Login login)
    {
        var response = await _sender.Send(new LoginQuery() { Email = login.email, PassWord = login.password });
        return Ok(response.Value);
    }
}
