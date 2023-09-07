using MediatR;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.UserManagement.Queries;

public record GetUserRoleQuery(string UserName, string Role) : IRequest<bool>;

public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, bool>
{
    private readonly IIdentityService _identityService;

    public GetUserRoleQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.IsInRoleAsync(request.UserName, request.Role);
    }
}
