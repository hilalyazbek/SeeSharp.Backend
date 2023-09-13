using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Application.Common.Models;
using SeeSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;
public class ChangePasswordCommand : IRequest<Result>
{
    public string? UserId { get;set; }

    public string? OldPassword { get; set;}

    public string? NewPassword { get; set; }
}

internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;

    public ChangePasswordCommandHandler(IIdentityService identityService, IApplicationDbContext context)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationUsers
            .FindAsync(new object[] { request.UserId! }, cancellationToken);

        Guard.Against.NotFound(request.UserId!, entity);

        return await _identityService.ChangePasswordAsync(request.UserId, request.OldPassword!, request.NewPassword!);
    }
}
