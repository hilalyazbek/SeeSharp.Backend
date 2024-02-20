using MediatR;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.BlogPosts.Queries.GetBlogPostByIdQuery;

public record GetBlogPostByIdQuery(Guid Id) : IRequest<BlogPostDto>;

public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto?>
{
    private readonly IApplicationDbContext _context;

    public GetBlogPostByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BlogPostDto?> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.BlogPosts
            .Select(b => new BlogPostDto
            {
                Id = b.Id,
                Title = b.Title,
                Category = b.Category,
                Author = b.Author.FullName,
                Content = b.Content,
                DateCreated = b.DateCreated.ToString("MMMM dd, yyyy")
            })
            .FirstOrDefaultAsync(itm => itm.Id == request.Id, cancellationToken);

        return result;
    }
}