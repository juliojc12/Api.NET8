using ClienteApi.Application.DTOs;

namespace ClienteApi.Application.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByIdAsync( Guid id );
        Task<ClienteDto> CreateAsync( ClienteDto clienteDto );
        Task<bool> UpdateAsync( Guid id, ClienteDto clienteDto );
        Task<bool> DeleteAsync( Guid id );
    }
}
