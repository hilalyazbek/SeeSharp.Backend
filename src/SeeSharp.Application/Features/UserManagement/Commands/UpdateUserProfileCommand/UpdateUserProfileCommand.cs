using Ardalis.GuardClauses;
using MediatR;
using SeeSharp.Application.Common.Interfaces;

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
    private readonly IApplicationDbContext _context;

    public UpdateUserProfileCommandHandler(IApplicationDbContext context)
    {
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
