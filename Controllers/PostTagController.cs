using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabloid.Data;
using Tabloid.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Tabloid.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Tabloid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostTagController : ControllerBase
{
    private readonly TabloidDbContext _dbContext;
    private readonly IMapper _mapper;

    public PostTagController(TabloidDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    public IActionResult NewPostTag(CreatePostTagDTO postTagDTO)
    {
        var postTag = _mapper.Map<PostTag>(postTagDTO);
        _dbContext.PostTags.Add(postTag);
        _dbContext.SaveChanges();
        return Created($"/api/posttag/{postTag.PostId}/{postTag.TagId}", _mapper.Map<PostTagDTO>(postTag));
    }

    [HttpDelete("{postId}/{tagId}")]
    [Authorize]
    public IActionResult DeletePostTag(int postId, int tagId)
    {
        PostTag? postTagToDelete = _dbContext.PostTags.SingleOrDefault(pt => pt.PostId == postId && pt.TagId == tagId);
        if (postTagToDelete == null)
        {
            return NotFound();
        }

        _dbContext.PostTags.Remove(postTagToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }
}