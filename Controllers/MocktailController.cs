using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetalPeel.Data;
using PetalPeel.Models;
using PetalPeel.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class MocktailController : ControllerBase
{
    private PetalPeelDbContext _dbContext;

    public MocktailController(PetalPeelDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    // [Authorize]

    public IActionResult GetMocktail()
    {
        return Ok(_dbContext.Mocktails
        .Include(m => m.Author)
        .Include(m => m.MocktailIngredients)
            .ThenInclude(mi => mi.Ingredient)
        .Include(m => m.MocktailIngredients)
            .ThenInclude(mi => mi.Quantity));
    }

     [HttpGet("{id}")]
    //[Authorize]
    public IActionResult GetMocktailById(int id)
    {
        Mocktail mocktail = _dbContext
            .Mocktails
            .Include(m => m.Author)
            .Include(m => m.MocktailIngredients)
                .ThenInclude(mi => mi.Ingredient)
            .Include(m => m.MocktailIngredients)
                .ThenInclude(mi => mi.Quantity)
            .SingleOrDefault(m => m.Id == id);

        if (mocktail == null)
        {
            return NotFound();
        }

        return Ok(mocktail);
    }

    [HttpDelete("{id}")]
    //[Authorize]
    public IActionResult DeleteMocktail(int id)
    {
        var Category = _dbContext.Mocktails.SingleOrDefault(c => c.Id == id);
        if (Category == null) return NotFound();

        _dbContext.Mocktails.Remove(Category);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> PostMocktail(Mocktail mocktail)
    {
        _dbContext.Mocktails.Add(mocktail);
        await _dbContext.SaveChangesAsync();

        return Created($"/api/category/{mocktail.Id}", mocktail);
    }

    [HttpPut("{id}")]
    //[Authorize]
    public async Task<IActionResult> UpdateMocktail(Mocktail mocktail, int id)
    {
        Mocktail MocktailToUpdate = _dbContext.Mocktails.SingleOrDefault(m => m.Id == id);
        if(MocktailToUpdate == null)
        {
            return NotFound();
        }
        else if(id != mocktail.Id)
        {
            return BadRequest();
        }

        //Editable Properties
        MocktailToUpdate.Name = mocktail.Name;
        MocktailToUpdate.Description = mocktail.Description;

        _dbContext.SaveChanges();

        return NoContent();

    }

}