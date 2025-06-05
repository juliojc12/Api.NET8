using ClienteApi.Domain.Entities;

namespace ClienteApi.Domain.Interfaces
{
    public interface IClienteRepository
    {

        Task<List<Cliente>> GetAsync();
        Task<Cliente?> GetByIdAsync();
        Task<Cliente?> GetByEmailAsync();
        Task Create( Cliente cliente );
        Task Update( Cliente cliente );
        Task Delete( Guid id );
        Task<bool> ExistAsync( Guid id );
    }
}
