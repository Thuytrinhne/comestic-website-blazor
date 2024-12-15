using FurnitureStore.Client.Requests;
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

        public async Task<bool> UpdateUserAddress(UserAddressDTO UserAddress, Guid UserId)
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

        public async Task<UserDTO> GetUserInfor(Guid UserId)
        {
            string apiUrl =
        $"{GlobalConfig.USER_BASE_URL}/" + UserId;
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                var user = jsonObject["user"].ToObject<UserDTO>();
                return user!;
            }
            return null!;
        }
        public async Task<bool> UpdateUserAsync(Guid Id, UpdateUserRequest user)
        {

            string apiUrl = $"{GlobalConfig.USER_BASE_URL}/" + Id;
            var response = await _httpClient.PatchAsJsonAsync(apiUrl, user);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateUserImageAsync(Guid Id, UpdateUserImageRequest user)
        {

            string apiUrl = $"{GlobalConfig.USER_BASE_URL}/" + Id+"/Image";
            using var content = new MultipartFormDataContent();

            if (user.Image != null)
            {
                var fileContent = new StreamContent(user.Image.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(user.Image.ContentType);
                content.Add(fileContent, "Image", user.Image.Name);
            }

            var response = await _httpClient.PatchAsync(apiUrl, content);

            return response.IsSuccessStatusCode;
        }
    }
}
