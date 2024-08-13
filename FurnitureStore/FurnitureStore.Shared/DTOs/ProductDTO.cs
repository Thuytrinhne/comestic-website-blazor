using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace FurnitureStore.Shared.DTOs;

public class ProductDTO
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("name")]

    public string Name { get; set; } = default!;

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("category")]
    public Category Category { get; set; }
    [JsonProperty("image")]
    public Image Image { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("tags")]
    public string Tags { get; set; }

    [JsonProperty("ratingCount")]
    public int RatingCount { get; set; }

    [JsonProperty("averageRating")]
    public decimal AverageRating { get; set; }
}

public class Image
{
    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonProperty("publicId")]
    public string PublicId { get; set; }
}
