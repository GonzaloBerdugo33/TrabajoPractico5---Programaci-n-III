using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using MudBlazor.Services;
using PuntoDeVenta.Movil.Auth;
using PuntoDeVenta.Movil.Servicios;
using PuntoDeVenta.Movil.Servicios.Interfaces;

namespace PuntoDeVenta.Movil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddMudServices();

            // El carrito vive mientras la app está abierta
            builder.Services.AddSingleton<CarritoService>();

            // HttpClient que apunta a la API y agrega el token en cada pedido
            builder.Services.AddTransient<JwtInterceptorHandler>();
            builder.Services.AddHttpClient("ApiHttpClient", client =>
            {
                client.BaseAddress = new Uri("https://miapitp.runasp.net/");
            })
            .AddHttpMessageHandler<JwtInterceptorHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiHttpClient"));

            // Login y roles
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            return builder.Build();
        }
    }
}
