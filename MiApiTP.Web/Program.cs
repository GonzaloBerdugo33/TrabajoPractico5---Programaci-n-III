using Blazored.LocalStorage;
using MiApiTP.Web;
using MiApiTP.Web.Auth;
using MiApiTP.Web.Services;
using MiApiTP.Web.Servicios;
using MiApiTP.Web.Servicios.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 4000;
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.BrowserConsole()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddTransient<JwtInterceptorHandler>();


builder.Services.AddHttpClient("ApiHttpClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? "https://localhost:7078/");
})
.AddHttpMessageHandler<JwtInterceptorHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiHttpClient"));

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IProductoService, ProductoService>();

await builder.Build().RunAsync();