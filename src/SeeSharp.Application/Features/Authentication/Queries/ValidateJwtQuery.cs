﻿using MediatR;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.Authentication.Queries;
public record ValidateJwtQuery(string Token) : IRequest<List<string>>;

public class ValidateJwtQueryHandler : IRequestHandler<ValidateJwtQuery, List<string>>
{
    private readonly IJwtUtils _jwtUtils;

    public ValidateJwtQueryHandler(IJwtUtils jwtUtils)
    {
        _jwtUtils = jwtUtils;
    }

    public Task<List<string>> Handle(ValidateJwtQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_jwtUtils.ValidateToken(request.Token));
    }
}