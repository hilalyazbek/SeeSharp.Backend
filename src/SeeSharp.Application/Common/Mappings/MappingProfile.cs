using System;
using AutoMapper;
using SeeSharp.Application.Features.BlogPosts.Queries;
using SeeSharp.Domain.Models;

namespace SeeSharp.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BlogPostDto, BlogPost>().ReverseMap();
    }
}

