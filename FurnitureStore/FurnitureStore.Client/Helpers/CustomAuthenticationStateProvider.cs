using Blazored.LocalStorage;
using FurnitureStore.Client.Services.IService;
using FurnitureStore.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FurnitureStore.Client.Helpers
{
    public class CustomAuthenticationStateProvider
        : AuthenticationStateProvider
    {
        public UserDTO CurrentUser { get; private set; } = new();
        private readonly IAuthService _authService;
        private readonly ILocalStorageService _localStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(IAuthService authService, ILocalStorageService localStorage)
        {
            _authService = authService;
             AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
            _localStorage = localStorage;
        }
        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
        public async Task<UserDTO> GetCurrentUserAsync()
        {
            await GetAuthenticationStateAsync();
            return CurrentUser;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrEmpty(token))
                {
                    return new AuthenticationState(_anonymous);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    return new AuthenticationState(_anonymous);
                }

                var claims = jwtToken.Claims.ToList();

                // Thêm các claims chuẩn
                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, claims.First(c => c.Type == "sub").Value),
                    new Claim(ClaimTypes.Name, claims.First(c => c.Type == "name").Value),
                    new Claim(ClaimTypes.Email, claims.First(c => c.Type == "email").Value)
                };

                // Thêm role claims
                var roleClaims = claims.Where(c => c.Type == "role")
                                      .Select(c => new Claim(ClaimTypes.Role, c.Value));
                userClaims.AddRange(roleClaims);

                var identity = new ClaimsIdentity(userClaims, "jwt");
                var user = new ClaimsPrincipal(identity);
                CurrentUser = new UserDTO
                {
                    Id = Guid.Parse(claims.First(c => c.Type == "sub").Value),
                    Email = claims.First(c => c.Type == "email").Value,
                    Name = claims.First(c => c.Type == "name").Value,
                    Roles = claims.Where(c => c.Type == "role")
                         .Select(c => c.Value)
                         .ToList()
                };

                return new AuthenticationState(user);
            }
            catch(Exception ex)
            {
                return new AuthenticationState(_anonymous);
            }
        }
        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                CurrentUser = UserDTO.FromClaimsPrincipal(authenticationState.User);
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = await _authService.Login(username, password);

            if (user is not null)
            {
                principal = user.ToClaimsPrincipal();
                CurrentUser = user;
                // here
                await GetAuthenticationStateAsync();
            }
            else return false;

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            return true;
        }
        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            CurrentUser = new();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }


    }
}
