using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Constants;

namespace SeeSharp.Application.Features.UserManagement.Queries;

public record GetUserIsAdminQuery(string UserId) : IRequest<bool>;

public class GetUserIsAdminQueryHandler : IRequestHandler<GetUserIsAdminQuery, bool>
{
    private readonly IIdentityService _identityService;

    public GetUserIsAdminQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(GetUserIsAdminQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.IsInRoleAsync(request.UserId, Roles.Administrator);
    }
}
