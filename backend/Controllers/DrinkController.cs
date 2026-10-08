using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace backend.Controllers;

[ApiController]
[Route("api/drinks")]
public class DrinksController : ControllerBase
{
    private readonly MongoDbClient _mongoClient;

    public DrinksController()
    {
        _mongoClient = MongoDbClient.Instance;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Drink>> GetById(string id)
    {
        var drinksCollection = _mongoClient.GetCollection<Drink>("Drinks");
        var drink = await drinksCollection.Find(d => d.Id == id).FirstOrDefaultAsync();

        if (drink == null)
            return NotFound(new { message = $"Напій з ID {id} не знайдено." });

        return Ok(drink);
    }

    [HttpPost("by-ingredients")] 
    public async Task<ActionResult<List<Drink>>> GetByIngredients([FromBody] List<string> userIngredients)
    {
        if (userIngredients == null || !userIngredients.Any())
            return BadRequest(new { message = "Передайте хоча б один інгредієнт." });

        var drinksCollection = _mongoClient.GetCollection<Drink>("Drinks");
        var allDrinks = await drinksCollection.Find(_ => true).ToListAsync();

        var userSet = userIngredients
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .Select(i => i.Trim().ToLower())
            .ToHashSet();

        var availableDrinks = allDrinks
            .Where(drink => drink.Ingredients != null
                         && drink.Ingredients.Any(ing => userSet.Contains(ing.Trim().ToLower())))
            .ToList();

        return Ok(availableDrinks);
    }
}