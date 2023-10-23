using Grpc.Core;
using MediatR;
using SeeSharp.Application.Features.BlogPosts.Commands.CreateBlogPost;
using SeeSharp.Application.Features.BlogPosts.Queries;
using SeeSharp.gRPC.Protos;

namespace SeeSharp.gRPC.Services;

public class BlogPostsService : BlogPosts.BlogPostsBase
{
    private readonly IMediator _mediator;

    public BlogPostsService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CreateBlogPostResponse> CreateBlogPost(CreateBlogPostRequest request, ServerCallContext context)
    {
        var command = new CreateBlogPostCommand
        {
            Title = request.Title,
            Author = request.Author,
            Category = request.Category,
            Content = request.Content
        };
        var result = await _mediator.Send(command);

        return await Task.FromResult(new CreateBlogPostResponse { Id = result.ToString() });
    }

    public override async Task<GetBlogPostsResponse> GetBlogPosts(GetBlogPostsRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetBlogPostsQuery());

        var response = new GetBlogPostsResponse();

        response.BlogPosts.AddRange(result.Select(blogpost => new GetBlogPostReponse
        {
            Id = blogpost.Id.ToString(),
            Title = blogpost.Title,
            Author = blogpost.Author,
            Category = blogpost.Category,
            Content = blogpost.Content,
            DateCreated = blogpost.DateCreated

        }));

        return await Task.FromResult(response);
    }
}
