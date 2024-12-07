namespace Ordering.Application.Dtos
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
    }
    
}
