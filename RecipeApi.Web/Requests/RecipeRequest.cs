using RecipeApi.Core;

namespace RecipeApi.Requests;

public class RecipeRequest
{
    public string? Name { get; set; }
    public List<Ingredient>? Ingredients { get; set; }
    public string? Description { get; set; }
}
