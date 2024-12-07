using FurnitureStore.Shared.DTOs;
using Ordering.Application.Dtos;

namespace FurnitureStore.Shared.Responses;

public class OrderResponse
{
    [JsonProperty("data")]
    public List<OrderDto> Data { get; set; }

    [JsonProperty("metadata")]
    public Metadata Metadata { get; set; }
}
