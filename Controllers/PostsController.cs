using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<Post> posts = _db.Posts
            .Where(post => post.Approved == true)
            .Where(post => post.PubDate <= DateTime.Now)
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
}