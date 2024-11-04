using Blazored.LocalStorage;
using FurnitureStore.Client.Services.IService;
using FurnitureStore.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FurnitureStore.Client.Services
{
    public class CustomAuthenticationStateProvider
        : AuthenticationStateProvider
    {
        public UserDTO CurrentUser { get; private set; } = new();
        private readonly IAuthService _authService;
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(IAuthService authService)
        {
            _authService = authService;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }
        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var claims = jwtToken.Claims.ToList();
                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                CurrentUser = UserDTO.FromClaimsPrincipal(user);

                return new AuthenticationState(user);
            }
            catch
            {
                await _localStorage.RemoveItemAsync("authToken");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
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
            }
            else return false;

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            return true;
        }


    }
}
