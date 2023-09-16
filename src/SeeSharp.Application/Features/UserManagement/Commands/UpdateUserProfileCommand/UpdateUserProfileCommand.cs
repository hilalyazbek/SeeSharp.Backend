using Ardalis.GuardClauses;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Application.Common.Models;

namespace SeeSharp.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;
public class UpdateUserProfileCommand : IRequest<Result>
{
    public string? UserId { get;set; }

    public string? FullName { get; set; }
}

internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserProfileCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationUsers
            .FindAsync(new object[] { request.UserId! }, cancellationToken);

        Guard.Against.NotFound(request.UserId!, entity);

        entity.FullName = request.FullName!;

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result.Success() : Result.Failure(new[] { "Failed up update user profile" });
    }
}
