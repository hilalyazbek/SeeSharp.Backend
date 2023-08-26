using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Features.BlogPosts.Commands;

public record CreateBlogPostCommand() : IRequest <Guid>
{
    public string? Title { get; set; }

    public string? Category { get; set; }

    public string? Author { get; set; }

    public string? Content { get; set; }
}

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogPostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = new BlogPost
        {
            Title = request.Title,
            Author = request.Author,
            Category = request.Category,
            Content = request.Content
        };

        _context.BlogPosts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}


