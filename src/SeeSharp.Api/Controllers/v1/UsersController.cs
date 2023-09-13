using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeeSharp.Application.Common.Models;
using SeeSharp.Application.Features.UserManagement.Commands.UpdatePasswordCommand;
using SeeSharp.Application.Features.UserManagement.Commands.UpdateEmailCommand;
using SeeSharp.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;
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

    [HttpGet("{id}")]
    public async Task<ApplicationUserDto> GetUserDetails(string id)
    {
        var query = new GetUserProfileDetailsQuery(id);

        return await _mediator.Send(query);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateUserDetails(string id, UpdateUserProfileCommand command)
    {
        if (id != command.UserId) return Results.BadRequest();

        await _mediator.Send(command);

        return Results.NoContent();
    }

    [HttpPost("{id}/UpdatePassword")]
    public async Task<Result> UpdatePassword(string id, UpdatePasswordCommand command)
    {
        if (id != command.UserId) return Result.Failure(new List<string>{"UserId Mismatch"});

        var result = await _mediator.Send(command);

        return result;
    }

    [HttpPost("{id}/UpdateEmail")]
    public async Task<Result> UpdateEmail(string id, UpdateEmailCommand command)
    {
        if (id != command.UserId) return Result.Failure(new List<string>{"UserId Mismatch"});

        var result = await _mediator.Send(command);

        return result;
    }

    [HttpGet("{id}/IsAdmin")]
    public async Task<bool> IsAdmin(string id){
        var query = new GetUserIsAdminQuery(id);
        return await _mediator.Send(query);
    }
}

