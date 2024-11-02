using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Requests;
using FurnitureStore.Shared.Responses;

namespace FurnitureStore.Client.Services.IService
{
    public interface IProductService
    {
        Task<ProductResponse> GetLatestProducts(int? pageNumber, int? pageSize);
        Task<bool> CreateProductAsync(CreateProductRequest product);
        Task<ProductDTO> GetProductById(string ProductId);
        Task<IEnumerable<ProductDTO>> GetProductListByProductIdList(List<string> listProductId);
        Task<ProductResponse> GetProductsByTitle(string title);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(string id);
        Task<ProductResponse> SearchProductByNameAsync(string Name);
        Task<ProductResponseById> SearchProductByRangeIdAsync(List<Guid> idRange);

    }
}
