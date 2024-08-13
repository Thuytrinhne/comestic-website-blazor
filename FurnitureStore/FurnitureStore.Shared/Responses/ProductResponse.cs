using FurnitureStore.Shared.DTOs;

namespace FurnitureStore.Shared.Responses;

public class ProductResponse
{
    
        [JsonProperty("productDtos")]
        public List<GroupedProducts> ProductDtos { get; set; }
   
}

public class GroupedProducts
{
    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("products")]

    public List<ProductDTO> Products { get; set; }
}
