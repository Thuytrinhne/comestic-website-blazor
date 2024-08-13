using FurnitureStore.Client.IServices;
using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Enums;
using FurnitureStore.Shared.Requests;
using FurnitureStore.Shared.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ordering.Application.Dtos;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace FurnitureStore.Client.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddOrderAsync(BasketCheckoutDTO basketCheckoutDTO, Guid UserId)
        {
            string apiUrl =
                   $"{GlobalConfig.CART_BASE_URL}/checkout";
            var data = new BasketCheckoutRequest(basketCheckoutDTO);
            var response = await _httpClient.PostAsJsonAsync(apiUrl, data);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<OrderDto?>> GetOrderByCustomerIdAsync(Guid UserId)
        {
            string apiUrl =
        $"{GlobalConfig.ORDER_BASE_URL}/customer/" + UserId;
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                return jsonObject["orders"].ToObject<IEnumerable<OrderDto>>()?? null!;

            }
            return null!;
        }

        public async Task<PaginationResult<OrderDto>> GetAllOrdersAsync()

        {
            string apiUrl =
        $"{GlobalConfig.ORDER_BASE_URL}";
            try
            {
                var response = await _httpClient.GetAsync(new Uri(apiUrl));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonResponse);
                    return jsonObject["paginationResult"].ToObject<PaginationResult<OrderDto>>() ?? null!;

                }
            }
            catch(Exception ex)
            {

            }
            return null!;
        }

        public async  Task<OrderDto> GetOrderByIdAsync( Guid OrderId)
        {
            string apiUrl =
       $"{GlobalConfig.ORDER_BASE_URL}/"+OrderId;
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                return jsonObject["order"].ToObject<OrderDto>() ?? null!;

            }
            return null!;
        }


        public async Task<bool> UpdateOrderStatusAsync(Guid OrderId, OrderStatus Status)
        {
            string apiUrl =
                   $"{GlobalConfig.ORDER_BASE_URL}/" + OrderId;
            var requestBody = new UpdateOrderRequest();
            requestBody.Order.Status = Status;
            var response = await _httpClient.PostAsJsonAsync(apiUrl, requestBody);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async  Task<PaginationResult<OrderDto>> FindOrdersByStatusAsync(OrderStatus orderStatus)
        {
            string apiUrl =
       $"{GlobalConfig.ORDER_BASE_URL}?StatusOrder="+ orderStatus;
            try
            {
                var response = await _httpClient.GetAsync(new Uri(apiUrl));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonResponse);
                    return jsonObject["paginationResult"].ToObject<PaginationResult<OrderDto>>() ?? null!;

                }
            }
            catch (Exception ex)
            {

            }
            return null!;
        }
    }
}
