using FurnitureStore.Shared.Enums;

namespace Ordering.Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderName { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public AddressDto BillingAddress { get; set; }
        public PaymentDto Payment { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public string Note { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderDto() { }

        public OrderDto(Guid id, Guid customerId, string orderName, AddressDto shippingAddress, AddressDto billingAddress, PaymentDto payment, OrderStatus status, List<OrderItemDto> orderItems, string note, DateTime dateOrder, decimal totalPrice)
        {
            Id = id;
            CustomerId = customerId;
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;
            OrderItems = orderItems;
            Note = note;
            DateOrder = dateOrder;
            TotalPrice = totalPrice;
        }
    }

}
