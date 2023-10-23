using Grpc.Core;
using MediatR;
using SeeSharp.Application.Features.BlogPosts.Commands.CreateBlogPost;
using SeeSharp.Application.Features.BlogPosts.Commands.DeleteBlogPost;
using SeeSharp.Application.Features.BlogPosts.Commands.UpdateBlogPost;
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

    public override async Task<GetBlogPostReponse> GetBlogPostById(GetBlogPostRequest request, ServerCallContext context)
    {
        var query = new GetBlogPostByIdQuery(Guid.Parse(request.Id));

        var result = await _mediator.Send(query);

        var response = new GetBlogPostReponse
        {
            Id = result.Id.ToString(),
            Title = result.Title,
            Category = result.Category,
            Author = result.Author,
            Content = result.Content,
            DateCreated = result.DateCreated
        };

        return await Task.FromResult(response);
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

    public override async Task<empty> UpdateBlogPost(UpdateBlogPostRequest request, ServerCallContext context)
    {
        var command = new UpdateBlogPostCommand
        {
            Id = Guid.Parse(request.Id),
            Title = request.Title,
            Category = request.Category,
            Author = request.Author,
            Content = request.Content,
        };

        await _mediator.Send(command);

        return new empty();
    }

    public override async Task<empty> DeleteBlogPost(DeleteBlogPostRequest request, ServerCallContext context)
    {
        var command = new DeleteBlogPostCommand(Guid.Parse(request.Id));

        await _mediator.Send(command);

        return new empty();
    }
}
