using Blazored.LocalStorage;
using FurnitureStore.Client.Services.IService;
using FurnitureStore.Client.Shared.Customer;
using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Responses;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace FurnitureStore.Client.Services
{
    public class AuthService
        (HttpClient _httpClient,
        ILocalStorageService _localStorageService) : IAuthService
    {
        public async  Task<bool> SendOTP(string email)
        {
            string apiUrl =
          $"{GlobalConfig.AUTH_BASE_URL}"+"/otps";

            var response = await _httpClient.PostAsJsonAsync(apiUrl, email);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<UserDTO> Login(string email, string password)
        {
            string apiUrl =
            $"{GlobalConfig.AUTH_BASE_URL}" + "/login";
            var loginData = new LoginRequest
            {
                Email = email,
                Password = password
            };
            var response = await _httpClient.PostAsJsonAsync(apiUrl, loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                var claimPrincipal = CreateClaimsPrincipalFromToken(result.Token);
                var user = UserDTO.FromClaimsPrincipal(claimPrincipal);
                PersistUserToBrowser(result.Token);
                return user;

            }
            else
            {
                return null;
            }
        }
        private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new(jwtSecurityToken.Claims, "Blazor School");
            }

            return new(identity);
        }

        private async  void PersistUserToBrowser(string token)
            => await _localStorageService.SetItemAsync("authToken", token);

    }
}
