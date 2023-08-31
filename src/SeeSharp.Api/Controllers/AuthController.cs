using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeeSharp.Application.Features.Authentication;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeeSharp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<string> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }
}

