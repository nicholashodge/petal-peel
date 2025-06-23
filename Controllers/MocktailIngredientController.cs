using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetalPeel.Data;
using PetalPeel.Models;

namespace PetalPeel.Controllers;

[Route("api/[controller]")]
[ApiController]

public class MocktailIngredientController : ControllerBase
{
    private  PetalPeelDbContext _dbContext;

    public MocktailIngredientController(PetalPeelDbContext context)
    {
        _dbContext = context;
    }

    [HttpDelete("mocktailingredient/{id}")]
    //[Authorize]
    public IActionResult DeleteMocktailIngredient(int id)
    {
        var mocktailIngredient = _dbContext.MocktailIngredients.Where(mi => mi.MocktailId == id).ToList();
        if(!mocktailIngredient.Any())
        {
            return NotFound();
        }

        _dbContext.MocktailIngredients.RemoveRange(mocktailIngredient);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost]
    //[Authorize]
    public IActionResult PostMocktailIngredient(MocktailIngredient mocktailIngredient)
    {
        _dbContext.MocktailIngredients.Add(mocktailIngredient);
        _dbContext.SaveChanges();
        return Created();
    }
}