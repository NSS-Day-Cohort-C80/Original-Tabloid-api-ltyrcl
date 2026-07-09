using System.Diagnostics.Tracing;
using AutoMapper;
using Tabloid.Models;
using Tabloid.Models.DTO;
using Tabloid.Models.DTOs;

namespace Tabloid;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<CreatePostDTO, Post>();
        CreateMap<Post, PostDetailsDTO>()
            .ForMember(destination => destination.UserName,
            options => options.MapFrom(source => source.User.IdentityUser.UserName));

        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<CreateCategoryDTO, Category>();

        CreateMap<Tag, TagDTO>().ReverseMap();
        CreateMap<CreateTagDTO, Tag>();

        CreateMap<Comment, CommentDTO>().ReverseMap();
        CreateMap<CreateCommentDTO, Comment>();

        CreateMap<Reaction, ReactionDTO>().ReverseMap();
        CreateMap<CreateReactionDTO, Reaction>();

        CreateMap<PostTag, PostTagDTO>().ReverseMap();

        CreateMap<Emoji, EmojiDTO>().ReverseMap();

        CreateMap<UserProfile, UserProfileDTO>();
    }
}