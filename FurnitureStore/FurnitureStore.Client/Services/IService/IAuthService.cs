using FurnitureStore.Client.Pages.Admin;
using FurnitureStore.Shared.DTOs;
using System.Security.Claims;

namespace FurnitureStore.Client.Services.IService
{
    public interface IAuthService
    {
        Task<bool> SendOTP (string email);
        Task<UserDTO> Login(string email, string password);
        Task<bool> Register(RegisterDto User);

    }
}
