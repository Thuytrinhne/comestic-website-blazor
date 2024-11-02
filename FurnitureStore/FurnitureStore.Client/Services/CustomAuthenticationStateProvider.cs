using Blazored.LocalStorage;
using FurnitureStore.Client.Services.IService;
using FurnitureStore.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FurnitureStore.Client.Services
{
    public class CustomAuthenticationStateProvider 
        : AuthenticationStateProvider
    {
        public UserDTO CurrentUser { get; private set; } = new();
        private readonly IAuthService _authService;
        public CustomAuthenticationStateProvider(IAuthService authService)
        {
            _authService = authService;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }
        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
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
