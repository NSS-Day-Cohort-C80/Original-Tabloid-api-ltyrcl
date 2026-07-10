using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;
using Tabloid.Data;
using Tabloid.Models;
using Tabloid.Models.DTOs;
using System.Security.Claims;
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
            .Include(post => post.Category)
            .Include(post => post.User)
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


    //view post details

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        Post post = _db.Posts
            .Include(post => post.User)
                .ThenInclude(user => user.IdentityUser)
            .SingleOrDefault(post => post.Id == id);

        if(post == null)
        {
            return NotFound();
        }

        PostDetailsDTO postDetailsDTO = _mapper.Map<PostDetailsDTO>(post);

        return Ok(postDetailsDTO);
    }

    //create a post
    
    [HttpPost]
    [Authorize]
    public IActionResult Create(CreatePostDTO createPostDTO)
    {
        Post post = _mapper.Map<Post>(createPostDTO);

        bool isAdmin = User.IsInRole("Admin");

        if (isAdmin)
        {
            post.Approved = true;
        }
        else
        {
            post.Approved = false;
        }

        _db.Posts.Add(post);
        _db.SaveChanges();

        PostDTO postDTO = _mapper.Map<PostDTO>(post);

        return CreatedAtAction(nameof(GetById), new { id = post.Id }, postDTO);
    }

    //edit post

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, PostDTO postDTO)
    {
        Post post = _db.Posts.SingleOrDefault(post => post.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var currentProfile = _db.UserProfiles.SingleOrDefault(up => up.IdentityUserId == identityUserId);
        if (!User.IsInRole("Admin") && post.UserId != currentProfile?.Id)
        {
            return Forbid();
        }

        post.Title = postDTO.Title;
        post.Body = postDTO.Body;
        post.CategoryId = postDTO.CategoryId;

        _db.SaveChanges();

        return NoContent();
    }


    //approve a post as an admin

    [HttpPut("{id}/approve")]
    [Authorize(Roles = "Admin")]
    public IActionResult Approve(int id)
    {
        Post post = _db.Posts.SingleOrDefault(post => post.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        post.Approved = true;

        _db.SaveChanges();

        return NoContent();
    }

    //un-approve a post as an admin

    [HttpPut("{id}/unapprove")]
    [Authorize(Roles = "Admin")]
    public IActionResult Unapprove(int id)
    {
        Post post = _db.Posts.SingleOrDefault(post => post.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        post.Approved = false;

        _db.SaveChanges();

        return NoContent();
    }
}