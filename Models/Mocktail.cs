using System.ComponentModel.DataAnnotations;

namespace PetalPeel.Models;

public class Mocktail
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public string? Instructions { get; set; }

    public int AuthorId { get; set; }
    public UserProfile? Author { get; set; }

    public List<MocktailIngredient>? MocktailIngredients { get; set; }

}