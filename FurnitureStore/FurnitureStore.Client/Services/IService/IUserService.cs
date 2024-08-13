using FurnitureStore.Shared.DTOs;

namespace FurnitureStore.Client.Services.IService
{
    public interface IUserService
    {
        Task<List<UserAddressDTO>> GetAllUserAddress(Guid UserId);
        Task<UserAddressDTO> GetDefaultUserAddress(Guid UserId);

        Task<bool> AddUserAddress(UserAddressDTO UserAddress , Guid UserId);
        Task<bool> DeleteUserAddress(Guid UserAddressId, Guid UserId);
        Task<bool> UpdateUserAddress(UserAddressDTO UserAddress, Guid UserId);
    }
}
