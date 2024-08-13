namespace FurnitureStore.Shared.DTOs
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; } = default!;
        public string Tags { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public Guid ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
    }
}
