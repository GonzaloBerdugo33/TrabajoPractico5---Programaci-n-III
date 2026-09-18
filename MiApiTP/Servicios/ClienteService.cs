using MiApiTP.Modelos;
using MiApiTP.Repositorios;

namespace MiApiTP.Servicios
{
    public class ClienteService
    {
        private readonly IClienteRepository _repositorio;

        public ClienteService(IClienteRepository repositorio) 
        {
            _repositorio = repositorio;
        }

        public async Task<List<Cliente>> ObtenerPaginado(int pagina, int tamanoPagina)
        {
            return await _repositorio.ObtenerPaginadoAsync(pagina, tamanoPagina);
        }

        public async Task<Cliente> ObtenerPorId(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task CrearCliente(Cliente cliente)
        {
            await _repositorio.AgregarAsync(cliente);
        }

        public async Task ActualizarCliente(Cliente cliente)
        {
            await _repositorio.ActualizarAsync(cliente);
        }

        public async Task EliminarCliente(int id)
        {
            await _repositorio.EliminarAsync(id);
        }
    }
}
