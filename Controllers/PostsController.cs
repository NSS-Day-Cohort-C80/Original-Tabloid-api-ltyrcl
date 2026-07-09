using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;
using Tabloid.Data;
using Tabloid.Models;
using Tabloid.Models.DTOs;
namespace Tabloid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly TabloidDbContext _db;
    private readonly IMapper _mapper;

    public PostsController(TabloidDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }


    //view all posts
    //int? tagId , int? categoryId

    [HttpGet]
    [Authorize]
    public IActionResult Get(int? categoryId, int? tagId)
    {
        IQueryable<Post> query = _db.Posts
            .Include(post => post.PostTags)
            .Where(post => post.Approved)
            .Where(post => post.PubDate <= DateTime.Now);

        if (categoryId.HasValue)
        {
            query = query.Where(post => post.CategoryId == categoryId.Value);

        }

        if (tagId.HasValue)
        {
            query = query.Where(post => post.PostTags.Any(postTag => postTag.TagId == tagId.Value));

        }

        List<Post> posts = query
            .OrderByDescending(post => post.PubDate)
            .ToList();

        List<PostDTO> postDTOs = _mapper.Map<List<PostDTO>>(posts);

        return Ok(postDTOs);
    }

    //view specific user's posts

    [HttpGet("myPosts/{userId}")]
    [Authorize(Roles = "User")]
    public IActionResult GetMyPosts(int userId)
    {
        List<Post> posts = _db.Posts
            .Where(post => post.UserId == userId)
            .OrderByDescending(post => post.PubDate)
            .ToList();

            List<PostDTO> postDTOs = _mapper.Map<List<PostDTO>>(posts);


            return Ok(postDTOs);
    }

    //edit post

    [HttpPut("{id}")]
    [Authorize(Roles = "User")]
    public IActionResult Update(int id, PostDTO postDTO)
    {
        Post post = _db.Posts.SingleOrDefault(post => post.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        post.Title = postDTO.Title;
        post.Body = postDTO.Body;
        post.CategoryId = postDTO.CategoryId;

        _db.SaveChanges();

        return NoContent();
    }

}