using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Application.Features.BlogPosts.Queries;
public class BlogPostsQueryParameters
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "DateCreated";
    public string? SortDirection { get; set; } = "Desc";
    public string? FilterByCategory { get; set; }
}
