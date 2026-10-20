using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using MiApiTP.Web.DTOs;
using MiApiTP.Web.Servicios.Interfaces;
using Microsoft.Extensions.Logging;

namespace MiApiTP.Web.Servicios
{
    public class ProductoService : IProductoService
    {

        private readonly HttpClient _http;
        private readonly ILogger<ProductoService> _logger;
        public ProductoService(HttpClient http, ILogger<ProductoService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<ProductoDTO>?> ObtenerPaginadoAsync(int pagina = 1, int tamanoPagina = 10)
        {
            try
            {
                _logger.LogInformation("Obteniendo productos paginados. Página: {Pagina}, Tamaño: {Tamano}", pagina, tamanoPagina);
                return await _http.GetFromJsonAsync<List<ProductoDTO>>($"api/Producto?pagina={pagina}&tamanoPagina={tamanoPagina}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista paginada de productos.");
                return null;
            }
        }

        public async Task<ProductoDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<ProductoDTO>($"api/Producto/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CrearProductoAsync(CrearProductoDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/Producto", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SubirImagenAsync(int productoId, IBrowserFile archivo)
        {
            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(archivo.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);

            content.Add(fileContent, "archivo", archivo.Name);

            var response = await _http.PostAsync($"api/Producto/{productoId}/imagen", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarProductoAsync(int id, CrearProductoDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"api/Producto/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Producto/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
