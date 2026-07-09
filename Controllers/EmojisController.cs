using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabloid.Data;
using Tabloid.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Tabloid.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Tabloid.Models.DTO;

namespace Tabloid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmojisController : ControllerBase
{
    private readonly TabloidDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmojisController(TabloidDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllEmojis()
    {
        var emojis = _dbContext
            .Emojis
            .ToList();

        return Ok(_mapper.Map<List<EmojiDTO>>(emojis));
    }
}