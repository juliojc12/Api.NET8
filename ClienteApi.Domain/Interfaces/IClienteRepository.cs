using ClienteApi.Domain.Entities;
using ClienteApi.Domain.ValueObjects;

namespace ClienteApi.Domain.Interfaces
{
    public interface IClienteRepository
    {

        Task<List<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync( Guid id );
        Task<Cliente?> GetByEmailAsync( Email email );
        Task CreateAsync( Cliente cliente );
        Task UpdateAsync( Cliente cliente );
        Task DeleteAsync( Guid id );
    }
}
