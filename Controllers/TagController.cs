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
    public IActionResult GetTags()
    {
        var tags = _dbContext
            .Tags
            .ToList();

        return Ok(_mapper.Map<List<TagDTO>>(tags));
    }
}