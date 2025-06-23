namespace PetalPeel.Models;

public class MocktailIngredient {
    public int Id { get; set; }
    public int MocktailId { get; set; }
    public Mocktail? Mocktail { get; set; }
    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    public double Quantity { get; set; }
}