using MediatR;
using SeeSharp.Application.Features.BlogPosts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.GraphQL;
public class Query
{
    public Query()
    {
    }

    public async Task<List<BlogPostDto>> GetBlogPosts([Service] IMediator mediatr)
    {
        return await mediatr.Send(new GetBlogPostsQuery());
    }
}
