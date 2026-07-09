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
public class ReactionsController : ControllerBase
{
    private readonly TabloidDbContext _dbContext;
    private readonly IMapper _mapper;
    public ReactionsController(TabloidDbContext db, IMapper mapper)
    {
        _dbContext = db;
        _mapper = mapper;
    }


    //view reactions on a post

    [HttpGet]
    [Authorize]
    public IActionResult GetReactions(int? postId)
    {
        var query = _dbContext
            .Reactions
            .Include(reaction => reaction.Emoji)
            .AsQueryable();

        if (postId.HasValue)
        {
            query = query.Where(reaction => reaction.PostId == postId.Value);
        }

        List<ReactionCountDTO> reactionCounts = query
            .GroupBy(reaction => reaction.Emoji.Symbol)
            .Select(group => new ReactionCountDTO
            {
                Emoji = group.Key,
                Count = group.Count()
            })
            .ToList();

        return Ok(reactionCounts);
    }


    //create a reaction

    [HttpPost]
    [Authorize]
    public IActionResult CreateReaction(CreateReactionDTO reactionDTO)
    {
        Reaction reaction = _mapper.Map<Reaction>(reactionDTO);

        _dbContext.Reactions.Add(reaction);
        _dbContext.SaveChanges();

        return Created($"/api/reactions/{reaction.Id}", reaction);
    }

    //delete a reaction (only if the user has added that reaction)

    [HttpDelete]
    [Authorize]
    public IActionResult RemoveReaction(int postId, int emojiId, int userId)
    {
        Reaction reaction = _dbContext.Reactions
            .SingleOrDefault(reaction =>
                reaction.PostId == postId &&
                reaction.EmojiId == emojiId &&
                reaction.UserId == userId);

        if (reaction == null)
        {
            return NotFound();
        }

        _dbContext.Reactions.Remove(reaction);
        _dbContext.SaveChanges();

        return NoContent();
    }
}