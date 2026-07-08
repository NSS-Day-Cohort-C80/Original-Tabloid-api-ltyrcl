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
public class TagController : ControllerBase
{
    private readonly TabloidDbContext _dbContext;
    private readonly IMapper _mapper;

    public TagController(TabloidDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllTags()
    {
        var tags = _dbContext
            .Tags
            .ToList();

        return Ok(_mapper.Map<List<TagDTO>>(tags));
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetTag(int id)
    {
        var tag = _dbContext
            .Tags
            .SingleOrDefault(t => t.Id == id);

        if (tag == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<TagDTO>(tag));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult EditTag([FromBody] TagDTO updatedTag, int id)
    {
        Tag? tagToUpdate = _dbContext.Tags.SingleOrDefault(t => t.Id == id);
        if (tagToUpdate == null)
        {
            return NotFound();
        }
        else if (id != updatedTag.Id)
        {
            return BadRequest();
        }

        tagToUpdate.Name = updatedTag.Name;
        
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult NewTag(CreateTagDTO tagDTO)
    {
        var tag = _mapper.Map<Tag>(tagDTO);
        _dbContext.Tags.Add(tag);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, _mapper.Map<TagDTO>(tag));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteTag(int id)
    {
        Tag? tagToDelete = _dbContext.Tags.SingleOrDefault(t => t.Id == id);
        if (tagToDelete == null)
        {
            return NotFound();
        }
        
        _dbContext.Tags.Remove(tagToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }
}