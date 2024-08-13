using FurnitureStore.Shared.DTOs;

namespace FurnitureStore.Client.Services.IService
{
    public interface IBasketService
    {
        Task<ShoppingCart> GetCartByUsername(Guid UserId);
        Task<bool> StoreShoppingCart(ShoppingCart cart);
        bool ShowCart { get; set; }
        event Action OnChange;
        event Action OnShowCartChanged; // Thêm sự kiện mới
        void NotifyStateChanged();

    }
}
