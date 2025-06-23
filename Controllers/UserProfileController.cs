using Microsoft.AspNetCore.Mvc;
using PetalPeel.Models;
using PetalPeel.Models.DTOs;
using PetalPeel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace PetalPeel.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private PetalPeelDbContext _dbContext;

    public UserProfileController(PetalPeelDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    //[Authorize]
    public IActionResult Get()
    {
        return Ok(_dbContext.UserProfiles.ToList());
    }

    [HttpGet("withroles")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetWithRoles()
    {
        return Ok(_dbContext.UserProfiles
        .Include(up => up.IdentityUser)
        .Select(up => new UserProfile
        {
            Id = up.Id,
            FirstName = up.FirstName,
            LastName = up.LastName,
            Email = up.IdentityUser.Email,
            UserName = up.IdentityUser.UserName,
            IdentityUserId = up.IdentityUserId,
            Roles = _dbContext.UserRoles
            .Where(ur => ur.UserId == up.IdentityUserId)
            .Select(ur => _dbContext.Roles.SingleOrDefault(r => r.Id == ur.RoleId).Name)
            .ToList()
        }));
    }

    [HttpGet("me")]
    //[Authorize]
    public IActionResult GetCurrentUser()
    {
        var identityUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);

        var user = _dbContext.UserProfiles
            .Include(up => up.IdentityUser)
            .SingleOrDefault(up => up.IdentityUserId == identityUserId);

        if (user == null)
        {
            return NotFound();
        }

        var dto = new UserProfileDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.IdentityUser.Email,
            UserName = user.IdentityUser.UserName,
            Roles = _dbContext.UserRoles
                .Where(ur => ur.UserId == user.IdentityUserId)
                .Select(ur => _dbContext.Roles.FirstOrDefault(r => r.Id == ur.RoleId).Name)
                .ToList()
        };

        return Ok(dto);
    }


    [HttpPost("promote/{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Promote(string id)
    {
        IdentityRole role = _dbContext.Roles.SingleOrDefault(r => r.Name == "Admin");
        _dbContext.UserRoles.Add(new IdentityUserRole<string>
        {
            RoleId = role.Id,
            UserId = id
        });
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPost("demote/{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Demote(string id)
    {
        IdentityRole role = _dbContext.Roles
            .SingleOrDefault(r => r.Name == "Admin");

        IdentityUserRole<string> userRole = _dbContext
            .UserRoles
            .SingleOrDefault(ur =>
                ur.RoleId == role.Id &&
                ur.UserId == id);

        _dbContext.UserRoles.Remove(userRole);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        UserProfile user = _dbContext
            .UserProfiles
            .Include(up => up.IdentityUser)
            .SingleOrDefault(up => up.Id == id);

        if (user == null)
        {
            return NotFound();
        }
        user.Email = user.IdentityUser.Email;
        user.UserName = user.IdentityUser.UserName;
        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserProfileDTO updatedUser)
    {
        var userProfile = await _dbContext.UserProfiles
            .Include(up => up.IdentityUser)
            .SingleOrDefaultAsync(up => up.Id == id);

        if (userProfile == null)
        {
            return NotFound();
        }

        // Update profile fields
        userProfile.FirstName = updatedUser.FirstName;
        userProfile.LastName = updatedUser.LastName;

        if (!string.IsNullOrEmpty(updatedUser.Email))
        {
            userProfile.IdentityUser.Email = updatedUser.Email;
            userProfile.IdentityUser.NormalizedEmail = updatedUser.Email.ToUpper();
        }

        if (!string.IsNullOrEmpty(updatedUser.UserName))
        {
            userProfile.IdentityUser.UserName = updatedUser.UserName;
            userProfile.IdentityUser.NormalizedUserName = updatedUser.UserName.ToUpper();
        }


        // Update password if provided
        if (!string.IsNullOrEmpty(updatedUser.Password))
        {
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
            var token = await userManager.GeneratePasswordResetTokenAsync(userProfile.IdentityUser);
            var result = await userManager.ResetPasswordAsync(userProfile.IdentityUser, token, updatedUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Password update failed.");
            }
        }

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }


}