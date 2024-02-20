using Ardalis.GuardClauses;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Features.BlogPosts.Commands.CreateBlogPost;

public record CreateBlogPostCommand() : IRequest<Guid>
{
    public string? Title { get; set; }

    public string? Category { get; set; }

    public string? Content { get; set; }
}

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentHttpRequest _currentHttpRequest;

    public CreateBlogPostCommandHandler(IApplicationDbContext context, ICurrentHttpRequest currentHttpRequest)
    {
        _context = context;
        _currentHttpRequest = currentHttpRequest;
    }

    public async Task<Guid> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var userId = Guard.Against.NullOrEmpty(_currentHttpRequest.GetUserId());
        var author = Guard.Against.Null(_context.ApplicationUsers.Find(userId));
        var entity = new BlogPost
        {
            Title = Guard.Against.NullOrEmpty(request.Title),
            AuthorId = userId,
            Author = author,
            Category = Guard.Against.NullOrEmpty(request.Category),
            Content = Guard.Against.NullOrEmpty(request.Content)
        };

        _context.BlogPosts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}


