using MediatR;
using SeeSharp.Application.Common.Exceptions;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Application.Features.Authentication.Commands;
public record AuthUserCommand : IRequest<AuthResponseDto>
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthUserCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
    {
        _identityService = identityService;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.AuthenticateAsync(request.UserName!, request.Password!);

        if(!result){
            throw new BadRequestException("Invalid username of password");
        }

        var (userId, userName, email, roles) = await _identityService.GetUserDetailsByUserNameAsync(request.UserName!);

        string token = _tokenGenerator.GenerateToken(userId,userName,roles);

        return new AuthResponseDto(){
            UserId = userId,
            Name = userName,
            Token = token
        };
    }
}