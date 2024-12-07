namespace FurnitureStore.Shared.DTOs;

public class Category
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Image Image { get; set; } = default!;

}
