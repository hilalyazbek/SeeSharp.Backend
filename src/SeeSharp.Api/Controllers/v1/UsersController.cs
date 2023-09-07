using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeeSharp.Application.Features.UserManagement.Queries;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeeSharp.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("UserIsInRole")]
    public async Task<bool> UserIsInRole([FromBody] GetUserRoleQuery query)
    {   
        return await _mediator.Send(query);
    }
}

