using RecipeApi.Requests;

namespace RecipeApi.Core;

public class Recipe
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public List<Ingredient>? Ingredients { get; set; }
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public Recipe(int id, RecipeRequest request)
  {
    Id = id;
    Name = request.Name;
    Ingredients = request.Ingredients;
    Description = request.Description;
    CreatedAt = DateTime.Now;
    UpdatedAt = DateTime.Now;
  }

  public Recipe() {}
}