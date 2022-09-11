using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Core;
using RecipeApi.Requests;

namespace RecipeApi.Controllers;

[ApiController]
[Route("recipes")]
public class RecipeController : ControllerBase
{
  private readonly IDataStore _db;


  public RecipeController(IDataStore db)
  {
    _db = db;
  }

  [HttpGet]
  public ActionResult GetAll()
  {
    var recipes = GetCollection().AsQueryable();
    return Ok(recipes);
  }

  [HttpGet("{id}", Name = "GetById")]
  public ActionResult GetById(int id)
  {
    var recipe = GetCollection()
      .Find(x => x.Id == id)
      .FirstOrDefault();
    
    if (recipe == null) return NotFound("Recipe not found");

    return Ok(recipe);
  }

  [HttpPost]
  public ActionResult Create(RecipeRequest request)
  {
    var collection = GetCollection();

    var recipe = new Recipe(collection.GetNextIdValue(), request);
    collection.InsertOne(recipe);

    return CreatedAtAction("GetById", new { id = recipe.Id }, recipe);
  }

  [HttpPut("{id}")]
  public ActionResult Update(int id, RecipeRequest request)
  {
    var didUpdate = GetCollection().UpdateOne(id, new {
      Name = request.Name,
      Ingredients = request.Ingredients,
      Description = request.Description,
      UpdatedAt = DateTime.Now
    });

    if (!didUpdate) return NotFound("Recipe not found");

    return Ok($"Recipe with id {id} updated");
  }

  [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    var didDelete = GetCollection().DeleteOne(id);

    if (!didDelete) return NotFound("Recipe not found");

    return NoContent();
  }

  private IDocumentCollection<Recipe> GetCollection() => _db.GetCollection<Recipe>("recipes");
}