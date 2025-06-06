using AutoMapper;
using ClienteApi.Application.Mapping;
using ClienteApi.Application.Services;
using ClienteApi.Domain.Entities;
using ClienteApi.Domain.Interfaces;
using ClienteApi.Domain.ValueObjects;
using Moq;

namespace ClienteAPI.testes
{
    public class ClienteServiceTestes
    {
        private readonly Mock<IClienteRepository> _mock;
        private readonly IMapper _mapper;
        private readonly ClienteService _clienteService;

        public ClienteServiceTestes()
        {
            _mock = new Mock<IClienteRepository>();

            var mapperConfig = new MapperConfiguration( cfg =>
            {
                cfg.AddProfile( new ClienteProfile() );
            } );

            _mapper = mapperConfig.CreateMapper();
            _clienteService = new ClienteService( _mock.Object, _mapper );
        }

        [Fact]
        public async Task GetAllAsync_RetrunAllClientes()
        {
            /*
             AAA
                Arenge
                Act
                Assert
             */

            //Arrange
            var clientes = new List<Cliente>
            {
                new Cliente(
                    Guid.NewGuid(),
                    "Sergio",
                    new Email("sergio@email.com"),
                    "1212312312",
                    new Endereco("Rua A", "123", "Rio de Janeiro", "SP", "1230123")
                    ),

                new Cliente(
                    Guid.NewGuid(),
                    "Andre",
                    new Email("andre@email.com"),
                    "1212312312",
                    new Endereco("Rua A", "321", "Rio de Janeiro", "SP", "23231123")
                    )
            };


            _mock.Setup( repo => repo.GetAllAsync() ).ReturnsAsync( clientes );


            //Act
            var result = await _clienteService.GetAllAsync();


            Assert.NotNull( result );
            Assert.Equal( 2, result.Count() );

            _mock.Verify( repo => repo.GetAllAsync(), Times.Once );

        }


        [Fact]
        public async Task GetClienteByIdAsync_ReturnCliente()
        {
            var clienteId = Guid.NewGuid();
            var cliente = new Cliente( clienteId, "João Silva", new Email( "joao.silva@example.com" ), "11987654321", new Endereco( "Rua A", "123", "São Paulo", "SP", "01000-000" ) );

            _mock.Setup( repo => repo.GetByIdAsync( clienteId ) ).ReturnsAsync( cliente );

            // Act
            var resultado = await _clienteService.GetByIdAsync( clienteId );

            Assert.NotNull( resultado );
            Assert.Equal( clienteId, resultado.Id );
            Assert.Equal( "João Silva", resultado.Nome );

            _mock.Verify( repo => repo.GetByIdAsync( clienteId ), Times.Once );

        }
    }
}