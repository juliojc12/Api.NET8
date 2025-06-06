using AutoMapper;
using ClienteApi.Application.DTOs;
using ClienteApi.Application.Interfaces;
using ClienteApi.Domain.Entities;
using ClienteApi.Domain.Interfaces;
using ClienteApi.Domain.ValueObjects;

namespace ClienteApi.Application.Services
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService( IClienteRepository repository, IMapper mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> GetAllAsync()
        {
            return _mapper.Map<List<ClienteDto>>( await _repository.GetAllAsync() );
        }

        public async Task<ClienteDto?> GetByIdAsync( Guid id )
        {
            var cliente = await _repository.GetByIdAsync( id );
            return cliente == null ? null : _mapper.Map<ClienteDto>( cliente );
        }

        public async Task<ClienteDto> CreateAsync( ClienteDto clienteDto )
        {
            Email email;

            try
            {
                email = new Email( clienteDto.Email );
            }
            catch (FormatException ex)
            {
                throw new ArgumentException( ex.Message, nameof( clienteDto.Email ) );
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException( ex.Message, nameof( clienteDto.Email ) );
            }

            var verificarEmail = await _repository.GetByEmailAsync( email );

            if (verificarEmail != null)
            {
                throw new InvalidOperationException( "Email em uso." );
            }


            var endereco = new Endereco(
                clienteDto.Endereco.Rua,
                clienteDto.Endereco.Numero,
                clienteDto.Endereco.Cidade,
                clienteDto.Endereco.Estado,
                clienteDto.Endereco.CEP
            );

            var cliente = new Cliente(
                clienteDto.Nome,
                email,
                clienteDto.Telefone,
                endereco );

            await _repository.CreateAsync( cliente );

            return _mapper.Map<ClienteDto>( cliente );

        }

        public async Task<ClienteDto> UpdateAsync( Guid id, ClienteDto clienteDto )
        {
            var cliente = await _repository.GetByIdAsync( id );
            if (cliente == null)
            {
                throw new ArgumentException( "Cliente não encontrado." );
            }

            Email novoEmail;

            try
            {
                novoEmail = new Email( clienteDto.Email );
            }
            catch (FormatException ex)
            {
                throw new ArgumentException( ex.Message, nameof( clienteDto.Email ) );
            }
            catch (ArgumentException ex) // Para emails nulos/vazios
            {
                throw new ArgumentException( ex.Message, nameof( clienteDto.Email ) );
            }



            if (novoEmail != cliente.Email)
            {
                var verificarEmail = await _repository.GetByEmailAsync( cliente.Email );
                if (verificarEmail != null)
                {
                    throw new InvalidOperationException( "Este email já encrontra-se em uso." );
                }
            }

            var endereco = new Endereco(
                clienteDto.Endereco.Rua,
                clienteDto.Endereco.Numero,
                clienteDto.Endereco.Cidade,
                clienteDto.Endereco.Estado,
                clienteDto.Endereco.CEP
            );

            cliente.Update( clienteDto.Nome, novoEmail, endereco, clienteDto.Telefone );

            await _repository.UpdateAsync( cliente );
            return _mapper.Map<ClienteDto>( cliente );
        }

        public async Task<bool> DeleteAsync( Guid id )
        {
            var cliente = await _repository.GetByIdAsync( id );
            if (cliente is null)
            {
                return false;
            }

            await _repository.DeleteAsync( id );

            return true;
        }

    }
}
