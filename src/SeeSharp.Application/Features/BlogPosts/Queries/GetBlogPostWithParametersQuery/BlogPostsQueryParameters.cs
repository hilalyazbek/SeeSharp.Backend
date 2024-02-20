namespace SeeSharp.Application.Features.BlogPosts.Queries.GetBlogPostWithParametersQuery;
public class BlogPostsQueryParameters
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "DateCreated";
    public string? SortDirection { get; set; } = "Desc";
    public string? Category { get; set; }
}
