namespace PetalPeel.Models.DTOs;

public class MocktailIngredientDTO {
    public int Id { get; set; }
    public int MocktailId { get; set; }
    public MocktailDTO? Mocktail { get; set; }
    public int IngredientId { get; set; }
    public IngredientDTO? Ingredient { get; set; }
}