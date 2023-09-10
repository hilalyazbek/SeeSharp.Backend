using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.BlogPosts.Queries;

public record GetBlogPostByIdQuery(Guid Id) : IRequest<BlogPostDto>;

public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogPostByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BlogPostDto> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.BlogPosts
            .FindAsync(new Object[] { request.Id }, cancellationToken);

        return _mapper.Map<BlogPostDto>(result);
            
    }
}