using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Enums;
using FurnitureStore.Shared.Responses;
using Ordering.Application.Dtos;

namespace FurnitureStore.Client.IServices
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(BasketCheckoutDTO basketCheckoutDTO, Guid UserId);
        Task<IEnumerable<OrderDto>> GetOrderByCustomerIdAsync(Guid UserId);
        Task<OrderDto> GetOrderByIdAsync( Guid OrderId);

        Task<PaginationResult<OrderDto>> GetAllOrdersAsync ();
        Task<PaginationResult<OrderDto>> FindOrdersByStatusAsync(OrderStatus orderStatus);
        Task<bool> UpdateOrderStatusAsync(Guid OrderId, OrderStatus Status);
    }
}
