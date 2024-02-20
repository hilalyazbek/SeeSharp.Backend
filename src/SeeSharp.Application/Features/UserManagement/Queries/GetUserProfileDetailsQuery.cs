using MediatR;
using SeeSharp.Application.Common.Exceptions;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.UserManagement.Queries;
public record GetUserProfileDetailsQuery(string UserId) : IRequest<ApplicationUserDto>;

internal class GetUserProfileDetailsQueryHandler : IRequestHandler<GetUserProfileDetailsQuery, ApplicationUserDto>
{
    private readonly IApplicationUserService _applicationUserService;

    public GetUserProfileDetailsQueryHandler(IApplicationUserService applicationUserService)
    {
        _applicationUserService = applicationUserService;
    }

    public async Task<ApplicationUserDto> Handle(GetUserProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        var (userId, fullName, userName, email, roles) = await _applicationUserService.GetUserDetailsByUserIdAsync(request.UserId);
        if (userId == null)
        {
            throw new NotFoundException($"User with UserID {request.UserId} does not exist");
        }

        var result = new ApplicationUserDto()
        {
            UserId = userId,
            FullName = fullName,
            Username = userName,
            Email = email,
            Roles = roles
        };

        return result;
    }
}