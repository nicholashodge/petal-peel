using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetalPeel.Data;
using PetalPeel.Models;
using PetalPeel.Models.DTOs;
using System.Security.Claims;


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
            .ThenInclude(mi => mi.Ingredient));
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
            .SingleOrDefault(m => m.Id == id);

        if (mocktail == null)
        {
            return NotFound();
        }

        return Ok(mocktail);
    }

    [HttpGet("mine")]
    [Authorize]
    public IActionResult GetMyMocktails()
    {
        var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var myMocktails = _dbContext.Mocktails
            .Include(m => m.Author)
            .Include(m => m.MocktailIngredients)
                .ThenInclude(mi => mi.Ingredient)
            .Where(m => m.Author.IdentityUserId == identityUserId)
            .ToList();

        return Ok(myMocktails);
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
    public async Task<IActionResult> PostMocktail([FromBody] MocktailDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userProfile = _dbContext.UserProfiles.FirstOrDefault(up => up.IdentityUserId == identityUserId);
        if (userProfile == null) return Unauthorized();

        if (dto.MocktailIngredients == null || !dto.MocktailIngredients.Any())
        {
            return BadRequest("Mocktail must have at least one ingredient.");
        }

        var mocktail = new Mocktail
        {
            Name = dto.Name,
            Description = dto.Description,
            Instructions = dto.Instructions,
            AuthorId = userProfile.Id,
            MocktailIngredients = dto.MocktailIngredients.Select(mi => new MocktailIngredient
            {
                IngredientId = mi.IngredientId,
                Quantity = mi.Quantity
            }).ToList()
        };

        _dbContext.Mocktails.Add(mocktail);
        await _dbContext.SaveChangesAsync();

        return Created($"/api/mocktail/{mocktail.Id}", mocktail);
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
        MocktailToUpdate.Instructions = mocktail.Instructions;

        _dbContext.SaveChanges();

        return NoContent();

    }

}