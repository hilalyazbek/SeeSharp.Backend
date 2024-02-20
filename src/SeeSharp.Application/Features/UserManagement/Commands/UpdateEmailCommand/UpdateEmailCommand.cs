using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Application.Common.Models;

namespace SeeSharp.Application.Features.UserManagement.Commands.UpdateEmailCommand;

public class UpdateEmailCommand : IRequest<Result>
{
    public string? UserId { get; set; }

    public string? Email { get; set; }
}

internal class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, Result>
{
    private readonly IIdentityService _identityService;

    public UpdateEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.UpdateEmailAsync(request.UserId!, request.Email!);
    }
}