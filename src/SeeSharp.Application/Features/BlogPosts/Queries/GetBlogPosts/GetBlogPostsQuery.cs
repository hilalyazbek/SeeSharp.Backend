using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Application.Features.BlogPosts.Queries.GetBlogPosts;

namespace SeeSharp.Application.Features.BlogPosts.Queries;

public record GetBlogPostsQuery() : IRequest<List<BlogPostDto>>;

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, List<BlogPostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogPostsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BlogPostDto>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        return await _context.BlogPosts
            .OrderByDescending(x => x.DateCreated)
            .ProjectTo<BlogPostDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

