using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models;

[BsonIgnoreExtraElements]
public class Drink
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore]
    public string MongoId { get; set; }

    [BsonElement("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [BsonElement("strength")]
    [JsonPropertyName("strength")]
    public string Strength { get; set; }

    [BsonElement("alcoholic")]
    [JsonPropertyName("alcoholic")]
    public string AlcoholicType { get; set; }

    [BsonIgnore]
    public bool IsAlcoholic => AlcoholicType?.Equals("Alcoholic", StringComparison.OrdinalIgnoreCase) ?? false;

    [BsonElement("imgUrl")]
    [JsonPropertyName("imgUrl")]
    public string ImgUrl { get; set; }

    [BsonElement("ingredients")]
    [JsonPropertyName("ingredients")]
    public List<string> Ingredients { get; set; } = new();

    [BsonElement("measure")]
    [JsonPropertyName("measure")]
    public List<string> Measure { get; set; } = new();

    [BsonIgnore]
    [JsonIgnore]
    public List<IngredientMeasure> CombinedIngredients =>
        Ingredients.Select((ing, index) => new IngredientMeasure
        {
            Ingredient = ing,
            Measure = index < Measure.Count ? Measure[index] : null
        }).ToList();
}

public class IngredientMeasure
{
    public string Ingredient { get; set; }
    public string Measure { get; set; }
}