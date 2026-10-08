using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
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

}