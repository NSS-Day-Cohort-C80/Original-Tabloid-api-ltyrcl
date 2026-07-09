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
public class CategoriesController : ControllerBase
{
    private readonly TabloidDbContext _dbContext;
    private readonly IMapper _mapper;

    public CategoriesController(TabloidDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllCategories()
    {
        var categories = _dbContext
            .Categories
            .ToList();

        return Ok(_mapper.Map<List<CategoryDTO>>(categories));
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetCategory(int id)
    {
        var category = _dbContext
            .Categories
            .SingleOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CategoryDTO>(category));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult EditCategory([FromBody] CategoryDTO updatedCategory, int id)
    {
        Category? categoryToUpdate = _dbContext.Categories.SingleOrDefault(c => c.Id == id);
        if (categoryToUpdate == null)
        {
            return NotFound();
        }
        else if (id != updatedCategory.Id)
        {
            return BadRequest();
        }

        categoryToUpdate.Name = updatedCategory.Name;
        
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult NewCategory(CreateCategoryDTO categoryDTO)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, _mapper.Map<CategoryDTO>(category));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteCategory(int id)
    {
        Category? categoryToDelete = _dbContext.Categories.SingleOrDefault(c => c.Id == id);
        if (categoryToDelete == null)
        {
            return NotFound();
        }

        if (_dbContext.Posts.Any(p => p.CategoryId == id))
        {
            return BadRequest("Cannot delete a category that still has posts.");
        }

        _dbContext.Categories.Remove(categoryToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }
}