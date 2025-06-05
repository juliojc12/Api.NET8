using ClienteApi.Domain.Entities;

namespace ClienteApi.Domain.Interfaces
{
    public interface IClienteRepository
    {

        Task<List<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync( Guid id );
        Task<Cliente?> GetByEmailAsync( string email );
        Task CreateAsync( Cliente cliente );
        Task UpdateAsync( Cliente cliente );
        Task DeleteAsync( Guid id );
    }
}
