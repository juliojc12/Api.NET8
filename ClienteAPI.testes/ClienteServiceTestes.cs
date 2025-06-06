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
                    "Sergio",
                    new Email("sergio@email.com"),
                    "1212312312",
                    new Endereco("Rua A", "123", "Rio de Janeiro", "SP", "1230123")
                    ),

                new Cliente(
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
    }
}