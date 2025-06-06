using ClienteApi.Application.DTOs;
using ClienteApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.API.Controllers
{


    [ApiController]
    [Route( "api/v1/clientes" )]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController( IClienteService service, ILogger<ClienteController> logger )
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            _logger.LogInformation( "Buscando todos os clientes" );
            var clientes = await _service.GetAllAsync();

            _logger.LogInformation( "{Count} clientes foram encontrados", clientes.Count() );
            return Ok( clientes );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetById( Guid id )
        {
            var cliente = await _service.GetByIdAsync( id );

            if (cliente == null)
            {
                _logger.LogError( "Cliente com ID {Id} não encontrado", id );
                return NotFound();
            }

            _logger.LogInformation( "Cliente {Nome} encontrado", cliente.Nome );
            return Ok( cliente );
        }

        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] ClienteDto dto )
        {
            try
            {
                var cliente = await _service.CreateAsync( dto );

                _logger.LogInformation( "Cliente cadastrado com sucesso: {Nome}", dto.Nome );
                return CreatedAtAction( nameof( GetById ), new { id = dto.Id }, cliente );
            }
            catch (ArgumentException error)
            {
                _logger.LogError( "Um erro inesperado aconteceu: {Message}", error.Message );
                return Conflict( new { message = error.Message } );
            }
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> Put( Guid id, [FromBody] ClienteDto dto )
        {
            var cliente = await _service.UpdateAsync( id, dto );

            if (cliente == null)
            {
                _logger.LogError( "Cliente com ID {Id} não encontrado", id );
                return NotFound();
            }

            _logger.LogInformation( "Informações do cliente com ID {Id} atualizadas com sucesso", id );
            return NoContent();
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> Delete( Guid id )
        {
            var cliente = await _service.DeleteAsync( id );

            if (!cliente)
            {
                _logger.LogError( "Cliente com ID {Id} não encontrado", id );
                return NotFound();
            }

            _logger.LogInformation( "Informações do cliente com ID {Id} excluídas com sucesso", id );
            return NoContent();
        }
    }
}
