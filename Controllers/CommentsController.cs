using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabloid.Data;
using Tabloid.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Tabloid.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Security.Claims;

namespace Tabloid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly TabloidDbContext _dbContext;
    private readonly IMapper _mapper;

    public CommentsController(TabloidDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetComments(int? postId)
    {
        var query = _dbContext
            .Comments
            .Include(c => c.User)
            .AsQueryable();

        if (postId.HasValue)
        {
            query = query.Where(c => c.PostId == postId.Value);
        }

        return Ok(_mapper.Map<List<CommentDTO>>(query.ToList()));
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetComment(int id)
    {
        var comment = _dbContext
            .Comments
            .SingleOrDefault(c => c.Id == id);

        if (comment == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CommentDTO>(comment));
    }

    [HttpPost]
    [Authorize]
    public IActionResult NewComment(CreateCommentDTO commentDTO)
    {
        if (!_dbContext.Posts.Any(p => p.Id == commentDTO.PostId))
        {
            return BadRequest("Cannot create a comment for a post that does not exist.");
        }

        if (!_dbContext.UserProfiles.Any(up => up.Id == commentDTO.UserId))
        {
            return BadRequest("Cannot create a comment for a user that does not exist.");
        }

        var comment = _mapper.Map<Comment>(commentDTO);
        _dbContext.Comments.Add(comment);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, _mapper.Map<CommentDTO>(comment));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteComment(int id)
    {
        Comment? commentToDelete = _dbContext.Comments.SingleOrDefault(c => c.Id == id);
        if (commentToDelete == null)
        {
            return NotFound();
        }

        var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var currentProfile = _dbContext.UserProfiles.SingleOrDefault(up => up.IdentityUserId == identityUserId);

        if (!User.IsInRole("Admin") && commentToDelete.UserId != currentProfile?.Id)
        {
            return Forbid();
        }

        _dbContext.Comments.Remove(commentToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }
}