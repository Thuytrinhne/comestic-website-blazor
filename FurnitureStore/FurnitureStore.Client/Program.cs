using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using FurnitureStore.Client;
using FurnitureStore.Client.Helpers;
using FurnitureStore.Client.IServices;
using FurnitureStore.Client.Services;
using FurnitureStore.Client.Services.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSweetAlert2(options =>
{
    options.Theme = SweetAlertTheme.Dark;
});

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:7000")
    });

#region trinh: dependency injection
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();


#endregion
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<LoadingService>();

await builder.Build().RunAsync();



