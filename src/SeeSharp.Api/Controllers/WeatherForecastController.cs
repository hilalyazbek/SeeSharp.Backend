using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeeSharp.Application.Features.BlogPosts.Commands;
using SeeSharp.Application.Features.BlogPosts.Queries;
using SeeSharp.Application.Features.BlogPosts.Queries.GetBlogPosts;

namespace SeeSharp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediatr;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<List<BlogPostDto>> Get()
    {
        return await _mediatr.Send(new GetBlogPostsQuery());
    }

    [HttpPost]
    public async Task<Guid> CreateBlogPost(CreateBlogPostCommand command)
    {
        return await _mediatr.Send(command);
    }
}

