using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Constants;

namespace SeeSharp.Application.Features.UserManagement.Queries;

public record UserIsAdminQuery(string UserId) : IRequest<bool>;

public class UserIsAdminQueryHandler : IRequestHandler<UserIsAdminQuery, bool>
{
    private readonly IIdentityService _identityService;

    public UserIsAdminQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(UserIsAdminQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.IsInRoleAsync(request.UserId, Roles.Administrator);
    }
}
