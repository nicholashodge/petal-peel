using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PetalPeel.Models;
using Microsoft.AspNetCore.Identity;
namespace PetalPeel.Data;

public class PetalPeelDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Mocktail> Mocktails { get; set; }
    public DbSet<MocktailIngredient> MocktailIngredients { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    public PetalPeelDbContext(DbContextOptions<PetalPeelDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}