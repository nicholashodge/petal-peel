using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetalPeel.Data;
using PetalPeel.Models;

namespace PetalPeel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
    private PetalPeelDbContext _dbContext;

    public IngredientController(PetalPeelDbContext context)
    {
        _dbContext = context;
    }
  
    [HttpGet]
    //[Authorize]
    public IActionResult GetIngredient()
    {
        return Ok(_dbContext.Ingredients.ToList());
    }

}