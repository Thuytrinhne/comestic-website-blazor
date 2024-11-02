
using FurnitureStore.Client.Services.IService;
using FurnitureStore.Client.Shared.Customer;
using FurnitureStore.Shared.DTOs;
using FurnitureStore.Shared.Requests;
using FurnitureStore.Shared.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace FurnitureStore.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductResponse> GetLatestProducts(int? pageNumber, int? pageSize)
        {

                string apiUrl =
       $"{GlobalConfig.PRODUCT_BASE_URL}" +
       $"?PageNumber={(pageNumber != null ? pageNumber : 0)}" +
       $"{(pageSize != null ? $"&PageSize={pageSize}" : 20)}";


                var response = await _httpClient.GetAsync(new Uri(apiUrl));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var productResponse = JsonConvert.DeserializeObject<ProductResponse>(jsonResponse);
                    return productResponse!;
                }
        
            return null!;


        }
        public async Task<ProductResponse> GetProductsByTitle(string title)
        {

            string apiUrl =
            $"{GlobalConfig.PRODUCT_BASE_URL}" +
            $"?Title={title}";
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var productResponse = JsonConvert.DeserializeObject<ProductResponse>(jsonResponse);
                return productResponse!;
            }


            return null!;

        }
        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(string id)
        {

            string apiUrl = $"{GlobalConfig.PRODUCT_BASE_URL}?categoryIds=" + id;
            var response = await _httpClient.GetAsync(new Uri(apiUrl));
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                var product = jsonObject["data"].ToObject<List<ProductDTO>>();
                return product!;
            }
            return null!;

        }

        public async Task<ProductResponse> SearchProductByNameAsync(string Name)
        {

            string apiUrl =
             $"{GlobalConfig.PRODUCT_BASE_URL}" +
             $"?Name={Name}";
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var productResponse = JsonConvert.DeserializeObject<ProductResponse>(jsonResponse);
                return productResponse!;
            }


            return null!;

        }
        public async Task<ProductDTO> GetProductById(string ProductId)
        {
            string apiUrl = $"{GlobalConfig.PRODUCT_BASE_URL}/" + ProductId;
            var response = await _httpClient.GetAsync(new Uri(apiUrl));
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductDTO>(jsonResponse);
                return product!;
            }
            return null!;
        }
        public async Task<IEnumerable<ProductDTO>> GetProductListByProductIdList(List<string> listProductId)
        {
            List<ProductDTO> result = new List<ProductDTO>();
            for (int i = 0; i < listProductId.Count; i++)
            {
                string apiUrl = $"{GlobalConfig.PRODUCT_BASE_URL}/" + listProductId[i];
                var response = await _httpClient.GetAsync(new Uri(apiUrl));
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductDTO>(jsonResponse);
                    if (product != null)
                        result.Add(product);
                }
            }
            return result!;
        }
        public async Task<IEnumerable<ProductDTO>> SearchProductByKeyWord(List<string> listProductId)
        {
            List<ProductDTO> result = new List<ProductDTO>();
            for (int i = 0; i < listProductId.Count; i++)
            {
                string apiUrl = $"{GlobalConfig.PRODUCT_BASE_URL}/" + listProductId[i];
                var response = await _httpClient.GetAsync(new Uri(apiUrl));
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductDTO>(jsonResponse);
                    if (product != null)
                        result.Add(product);
                }
            }
            return result!;
        }

        public async  Task<bool> CreateProductAsync(CreateProductRequest product)
        {

            string apiUrl = $"{GlobalConfig.PRODUCT_BASE_URL}";

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(product.Name), "Name");
            content.Add(new StringContent(product.Title), "Title");
            content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(product.Description), "Description");
            content.Add(new StringContent(product.Price.ToString()), "Price");
            content.Add(new StringContent(product.Tags), "Tags");

            if (product.Image != null)
            {
                var fileContent = new StreamContent(product.Image.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(product.Image.ContentType);
                content.Add(fileContent, "Image", product.Image.Name);
            }

            var response = await _httpClient.PostAsync(apiUrl, content);

            return response.IsSuccessStatusCode;
        }

        public async  Task<ProductResponseById> SearchProductByRangeIdAsync(List<Guid> idRange)
        {
            string apiUrl = $"{GlobalConfig.PRODUCT_BASE_URL}/search";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, idRange);


            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var productResponse = JsonConvert.DeserializeObject<ProductResponseById>(jsonResponse);
                return productResponse!;
            }

            return null!;
        }
    }
}
