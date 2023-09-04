using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeeSharp.Application.Features.Authentication.Commands.AuthUserCommand;
using SeeSharp.Application.Features.Authentication.Commands.CreateUserCommand;

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

    [HttpPost("Register")]
    public async Task<string> CreateUser([FromBody] CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("Login")]
    public async Task<AuthResponseDto> Login([FromBody] AuthUserCommand command)
    {
        return await _mediator.Send(command);
    }
}

