using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.BlogPosts.Queries;

public record GetBlogPostsQuery() : IRequest<List<BlogPostDto>>;

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, List<BlogPostDto>>
{
    private readonly IApplicationDbContext _context;

    public GetBlogPostsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogPostDto>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        return await _context.BlogPosts
            .OrderByDescending(x => x.DateCreated)
            .Select(b => new BlogPostDto
            {
                Id = b.Id,
                Title = b.Title,
                Category = b.Category,
                Author = b.Author.FullName,
                Content = b.Content,
                DateCreated = b.DateCreated.ToString("MMMM dd, yyyy")
            })
            .ToListAsync(cancellationToken);
    }
}

