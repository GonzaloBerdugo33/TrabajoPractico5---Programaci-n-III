using MiApiTP.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiApiTP.DTOs
{

    public class RegistroClienteDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }

    public class CrearCompraDTO
    {
        public string MetodoPago { get; set; } = "Tarjeta";
        public List<CrearSalidaDetalleDTO> Detalles { get; set; } = new();
    }
}
