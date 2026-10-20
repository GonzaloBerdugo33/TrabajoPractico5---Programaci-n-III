using MiApiTP.Web.DTOs;

namespace MiApiTP.Web.Servicios.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }
}
