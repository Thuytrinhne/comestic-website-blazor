namespace FurnitureStore.Client
{
    public class GlobalConfig
    {
        public const string BASE_ENDPOINT = "http://localhost:7000";
        public const string BASE_URL = $"{BASE_ENDPOINT}";
        public const string PRODUCT_BASE_URL = $"{BASE_URL}/product-api/products";
        public const string CART_BASE_URL = $"{BASE_URL}/basket-api/basket";
        public const string AUTH_BASE_URL = $"{BASE_URL}/auth-api/auth";
        public const string USER_BASE_URL = $"{BASE_URL}/user-api/users";
        public const string ORDER_BASE_URL = $"{BASE_URL}/order-api/orders";

        public const string LOCATION_URL = "https://vapi.vnappmob.com";



        public const string CATEGORY_BASE_URL = $"{BASE_URL}Categories";
        public const string STAFF_BASE_URL = $"{BASE_URL}Staffs";
    }
}
