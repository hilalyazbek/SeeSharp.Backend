using MediatR;
using SeeSharp.Application.Features.BlogPosts.Queries;
using SeeSharp.Application.Features.BlogPosts.Queries.GetBlogPostsQuery;

namespace SeeSharp.GraphQL;
public class Query
{
    public Query()
    {
    }

    [UsePaging(IncludeTotalCount = true)]
    [UseFiltering]
    public async Task<List<BlogPostDto>> GetBlogPosts([Service] IMediator mediatr)
    {
        return await mediatr.Send(new GetBlogPostsQuery());
    }
}
