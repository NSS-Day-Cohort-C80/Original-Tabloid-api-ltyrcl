using AutoMapper;
using Tabloid.Models;
using Tabloid.Models.DTOs;

namespace Tabloid;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<CreatePostDTO, Post>();

        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<CreateCategoryDTO, Category>();

        CreateMap<Tag, TagDTO>().ReverseMap();
        CreateMap<CreateTagDTO, Tag>();

        CreateMap<Comment, CommentDTO>().ReverseMap();
        CreateMap<CreateCommentDTO, Comment>();

        CreateMap<Reaction, ReactionDTO>().ReverseMap();
        CreateMap<CreateReactionDTO, Reaction>();

        CreateMap<UserProfile, UserProfileDTO>();
    }
}