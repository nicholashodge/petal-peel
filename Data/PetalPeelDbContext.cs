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

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            UserName = "Administrator",
            NormalizedUserName = "ADMINISTRATOR",
            Email = "admina@strator.comx",
            NormalizedEmail = "ADMINA@STRATOR.COMX",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEEkthWzE9RlmvYZa0Gl0ej4SzvCr2YJXzvMXVWlYYGrCDVV7sQKFMCHXs3PU0YZRaQ==",
            SecurityStamp = "admin-static-security-stamp",
            ConcurrencyStamp = "admin-static-concurrency-stamp"
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
        });

        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 1,
            IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            FirstName = "Admina",
            LastName = "Strator"
        });

        modelBuilder.Entity<Ingredient>().HasData(new Ingredient[]
        {
            new Ingredient
            {
                Id = 1,
                Name = "Club soda",
                Measurement = "ml"
            },
            new Ingredient
            {
                Id = 2,
                Name = "Mint leaves",
                Measurement = null
            },
            new Ingredient
            {
                Id = 3,
                Name = "Lime",
                Measurement = null
            },
            new Ingredient
            {
                Id = 4,
                Name = "Lime Juice",
                Measurement = "oz"
            },
            new Ingredient
            {
                Id = 5,
                Name = "Simple Syrup",
                Measurement = "oz"
            },
            new Ingredient
            {
                Id = 6,
                Name = "Cucumber juice",
                Measurement = "oz"
            },
            new Ingredient
            {
                Id = 7,
                Name = "Ginger Ale",
                Measurement = "oz"
            },
            new Ingredient
            {
                Id = 8,
                Name = "Raspberry Puree",
                Measurement = "ml"
            },
            new Ingredient
            {
                Id = 9,
                Name = "Sprite",
                Measurement = "ml"
            }
        });

        modelBuilder.Entity<MocktailIngredient>().HasData(new MocktailIngredient[]
        {
            new MocktailIngredient{Id = 1, MocktailId = 1, IngredientId = 2, Quantity = 2},
            new MocktailIngredient{Id = 2, MocktailId = 1, IngredientId = 4, Quantity = 0.5},
            new MocktailIngredient{Id = 3, MocktailId = 1, IngredientId = 5, Quantity = 0.5},
            new MocktailIngredient{Id = 4, MocktailId = 1, IngredientId = 7, Quantity = 6},
            new MocktailIngredient{Id = 5, MocktailId = 2, IngredientId = 8, Quantity = 30},
            new MocktailIngredient{Id = 6, MocktailId = 2, IngredientId = 2, Quantity = 4},
            new MocktailIngredient{Id = 7, MocktailId = 2, IngredientId = 4, Quantity = 5},
            new MocktailIngredient{Id = 8, MocktailId = 2, IngredientId = 9, Quantity = 100}
        });

        modelBuilder.Entity<Mocktail>().HasData(new Mocktail[]
        {
            new Mocktail
            {
                Id = 1,
                Name = "Cucumber Mule",
                Description = "A crisp and invigorating twist on the classic mule, this mocktail blends the refreshing essence of muddled cucumber with the zing of freshly squeezed lime juice and the bold snap of ginger beer. Served over ice in a copper mug and garnished with a cucumber ribbon and sprig of mint, the Cucumber Mule is the perfect cool-down companion—herbaceous, spicy, and delightfully effervescent.",
                AuthorId = 1
            },
            new Mocktail
            {
                Id = 2,
                Name = "Raspberry Mojito",
                Description = "A vibrant burst of summer in every sip, this non-alcoholic Raspberry Mojito fuses juicy raspberries with muddled mint and zesty lime for a refreshing twist on a Cuban classic. Topped with sparkling soda water and served over crushed ice, it’s a perfectly balanced blend of tart, sweet, and cool—finished with a mint sprig and a handful of fresh berries for a pop of color and flavor.",
                AuthorId = 1
            }
        });

    }

}