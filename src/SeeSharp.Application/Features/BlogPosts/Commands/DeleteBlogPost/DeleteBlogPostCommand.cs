using Ardalis.GuardClauses;
using MediatR;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.BlogPosts.Commands.DeleteBlogPost;

public record DeleteBlogPostCommand(Guid Id) : IRequest;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBlogPostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogPosts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.BlogPosts.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}