using HotChocolate;
using MediatR;
using SeeSharp.Application.Features.BlogPosts.Commands.CreateBlogPost;
using SeeSharp.Application.Features.BlogPosts.Commands.DeleteBlogPost;
using SeeSharp.Application.Features.BlogPosts.Commands.UpdateBlogPost;
using SeeSharp.Application.Features.BlogPosts.Queries;

namespace SeeSharp.Api.GraphQL;

public class Mutation
{
    public async Task<Guid> AddBlogPost(
        [Service] ISender mediatr,
        CreateBlogPostCommand command)
    {
        return await mediatr.Send(command);
    }

    public async Task<bool> UpdateBlogPost(
        [Service] ISender mediatr,
        Guid id,
        UpdateBlogPostCommand command)
    {
        if (id != command.Id) return false;
        await mediatr.Send(command);
        return true;
    }

    public async Task<bool> DeleteBlogPost(
        [Service] ISender mediatr,
        Guid id)
    {
        await mediatr.Send(new DeleteBlogPostCommand(id));
        return true;
    }
}
