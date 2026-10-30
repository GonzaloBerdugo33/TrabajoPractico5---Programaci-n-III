using System;
using System.Collections.Generic;
using System.Text;
using PuntoDeVenta.Movil.DTOs;

namespace PuntoDeVenta.Movil.Servicios.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }
}
