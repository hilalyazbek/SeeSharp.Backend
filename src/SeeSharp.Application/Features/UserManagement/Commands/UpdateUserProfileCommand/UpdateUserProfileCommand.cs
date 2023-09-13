using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;
public class UpdateUserProfileCommand : IRequest
{
    public string? UserId { get;set; }

    public string? FullName { get; set; }

    public string? UserName { get; set;}

    public string? OldPassword { get; set;}

    public string? NewPassword { get; set; }

    public string? UserEmail { get; set;}
}

internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;

    public UpdateUserProfileCommandHandler(IIdentityService identityService, IApplicationDbContext context)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationUsers
            .FindAsync(new object[] { request.UserId! }, cancellationToken);

        Guard.Against.NotFound(request.UserId!, entity);

        entity.FullName = request.FullName!;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
