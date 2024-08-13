using static FurnitureStore.Client.Pages.Customer.AddressManagement.AddAddress;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using FurnitureStore.Shared.DTOs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FurnitureStore.Client.Helpers
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;

        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Province>> GetProvinces()
        {
            string apiUrl =
         $"{GlobalConfig.LOCATION_URL}/api/province/";
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                return jsonObject["results"].ToObject<List<Province>>();

            }
            return null!;
        }


        public async Task<List<District>> GetDistricts(string provinceCode)
        {
            string apiUrl =
       $"{GlobalConfig.LOCATION_URL}/api/province/district/"+provinceCode;
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                return jsonObject["results"].ToObject<List<District>>();

            }
            return null!;
        }

        public async Task<List<Ward>> GetWards(string districtCode)
        {
            string apiUrl =
       $"{GlobalConfig.LOCATION_URL}/api/province/ward/"+districtCode;
            var response = await _httpClient.GetAsync(new Uri(apiUrl));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonResponse);
                return jsonObject["results"].ToObject<List<Ward>>();

            }
            return null!;
        }
    }
    public class Province
    {
       
        [JsonProperty("province_id")]

        public string ProvinceID { get; set; }
        [JsonProperty("province_name")]
        public string ProvinceName { get; set; }
    }

    public class District
    {
       
        [JsonProperty("district_id")]
        public string DistrictID { get; set; }
        [JsonProperty("district_name")]
        public string DistrictName  { get; set; } 
    
    }

    public class Ward
    {
        [JsonProperty("ward_id")]

        public string WardID { get; set; }
        [JsonProperty("ward_name")]

        public string WardName { get; set; }
    }
}
