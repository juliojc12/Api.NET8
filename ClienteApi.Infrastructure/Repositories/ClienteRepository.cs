using ClienteApi.Domain.Entities;
using ClienteApi.Domain.Interfaces;
using ClienteApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClienteApi.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository( AppDbContext context )
    {
        _context = context;
    }

    public async Task<List<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync( Guid id )
    {
        return await _context.Clientes.FindAsync( id );
    }

    public async Task<Cliente?> GetByEmailAsync( string email )
    {
        return await _context.Clientes.FirstOrDefaultAsync( c => c.Email == email );
    }

    public async Task CreateAsync( Cliente cliente )
    {
        _context.Clientes.Add( cliente );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( Cliente cliente )
    {
        _context.Clientes.Update( cliente );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync( Guid id )
    {
        var cliente = await _context.Clientes.FindAsync( id );
        if (cliente != null)
        {
            _context.Clientes.Remove( cliente );
            await _context.SaveChangesAsync();
        }
    }
}