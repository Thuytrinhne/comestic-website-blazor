using FurnitureStore.Client.Services.IService;
using FurnitureStore.Client.Shared.Customer;
using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Requests;
using FurnitureStore.Shared.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FurnitureStore.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddUserAddress(UserAddressDTO UserAddress, Guid UserId)
        { 
          string apiUrl =
         $"{GlobalConfig.USER_BASE_URL}/" + UserId + "/addresses";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, UserAddress);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAddress(Guid UserAddressId, Guid UserId)
        {
            string apiUrl =
         $"{GlobalConfig.USER_BASE_URL}/" + UserId + "/addresses/"+ UserAddressId;
            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
              return false;

            }

        }

        public async  Task<List<UserAddressDTO>> GetAllUserAddress(Guid UserId)
        {
            string apiUrl =
         $"{GlobalConfig.USER_BASE_URL}/"+UserId + "/addresses";
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                return  jsonObject["listAddress"].ToObject<IEnumerable<UserAddressDTO>>().ToList() ?? null!;

            }
            return null!;
        }

        public async  Task<UserAddressDTO> GetDefaultUserAddress(Guid UserId)
        {
            string apiUrl =
        $"{GlobalConfig.USER_BASE_URL}/" + UserId + "/addresses?defaultAddress=1";
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                var addresses=  jsonObject["listAddress"].ToObject<IEnumerable<UserAddressDTO>>().ToList();
                return addresses[0] ?? new UserAddressDTO();

            }
            return null!;
        }

        public async  Task<bool> UpdateUserAddress(UserAddressDTO UserAddress, Guid UserId)
        {
            
                string apiUrl =
         $"{GlobalConfig.USER_BASE_URL}/" + UserId + "/addresses";
            var response = await _httpClient.PatchAsJsonAsync(apiUrl, UserAddress);
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
}
