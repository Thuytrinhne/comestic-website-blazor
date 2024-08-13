using FurnitureStore.Client.Services.IService;
using FurnitureStore.Client.Shared;
using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace FurnitureStore.Client.Services
{
    public class BasketService : IBasketService
    {
        public event Action OnChange;
        public event Action OnShowCartChanged; // Thêm sự kiện mới
        private bool _showCart = false;

        public bool ShowCart
        {
            get => _showCart;
            set
            {
                _showCart = value;
                NotifyStateChanged();
                if (_showCart== true)
                OnShowCartChanged?.Invoke();
            }
        }

        public void NotifyStateChanged() => OnChange?.Invoke();

        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ShoppingCart> GetCartByUsername(Guid UserId)
        {
            string apiUrl =
            $"{GlobalConfig.CART_BASE_URL}" + "/"+ UserId;
            try
            {
                var response = await _httpClient.GetAsync(new Uri(apiUrl));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonResponse);
                    var cart = jsonObject["cart"].ToObject<ShoppingCart>();
                    return cart!;
                }

            }
            catch (Exception ex) 
            {

            }
            return null!;
        }

        public async  Task<bool> StoreShoppingCart(ShoppingCart cart)
        {
            string apiUrl =
           $"{GlobalConfig.CART_BASE_URL}";
            var cartContainer = new CartContainer { Cart = cart };

            var response = await _httpClient.PostAsJsonAsync(apiUrl, cartContainer);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    public class CartContainer
    {
        public ShoppingCart Cart { get; set; }
    }
}
