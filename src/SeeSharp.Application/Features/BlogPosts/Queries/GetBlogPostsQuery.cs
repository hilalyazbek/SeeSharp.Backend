using MediatR;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.BlogPosts.Queries;

public class GetBlogPostsQuery : IRequest<List<BlogPostDto>>
{
    public BlogPostsQueryParameters QueryParameters { get; }

    public GetBlogPostsQuery(BlogPostsQueryParameters queryParameters)
    {
        QueryParameters = queryParameters;
    }
}

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, List<BlogPostDto>>
{
    private readonly IApplicationDbContext _context;

    public GetBlogPostsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogPostDto>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.BlogPosts.AsQueryable();

        // Apply filtering
        if (!string.IsNullOrEmpty(request.QueryParameters.Category))
        {
            query = query.Where(post => post.Category == request.QueryParameters.Category);
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(request.QueryParameters.SortBy))
        {
            var propertyName = request.QueryParameters.SortBy;
            var isAscending = string.Equals(request.QueryParameters.SortDirection, "asc", StringComparison.OrdinalIgnoreCase);

            query = isAscending
                ? query.OrderBy(post => post.DateCreated)
                : query.OrderByDescending(post => post.DateCreated);
        }
        else
        {
            query = query.OrderByDescending(post => post.DateCreated);
        }

        // Apply paging
        var skip = (request.QueryParameters.Page - 1) * request.QueryParameters.PageSize;
        var take = request.QueryParameters.PageSize;
        var result = await query
            .Skip(skip)
            .Take(take)
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

        return result;
    }
}

