using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Responses;

namespace FurnitureStore.Client.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategoryResponsesWithNestedResult();
        Task<Category> AddCategoryAsync(Category categoryDTO);
        Task<bool> DeleteCategoryDTOAsync(string categoryId);
        Task<bool> UpdateCategoryDTOAsync(string categoryId, Category categoryDTO);
        Task<Category> GetCategoryDTOByIdAsync(string categoryId);
        Task<IEnumerable<Category>> GetCategoryDTOsByLevelAsync(int level);
        Task<IEnumerable<Category>> GetCategoryDTOsByParentIdAsync(string parentId);
    }
}
