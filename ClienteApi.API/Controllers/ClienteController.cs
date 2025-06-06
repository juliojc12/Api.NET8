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
        public async Task<IActionResult> Get()
        {
            return Ok( await _service.GetAllAsync() );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetById( Guid id )
        {
            var cliente = await _service.GetByIdAsync( id );

            if (cliente == null)
            {
                _logger.LogError( "Cliente não encontrado" );
                return NotFound();
            }

            return Ok( cliente );
        }

        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] ClienteDto dto )
        {
            try
            {
                var cliente = await _service.CreateAsync( dto );
                return CreatedAtAction( nameof( GetById ), new { id = dto.Id }, cliente );
            }
            catch (ArgumentException error)
            {
                _logger.LogError( error.Message );
                return Conflict( new { message = error.Message } );
            }
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> Put( Guid id, [FromBody] ClienteDto dto )
        {
            var cliente = await _service.UpdateAsync( id, dto );

            if (cliente == null)
            {
                return NotFound();
            }
            _logger.LogInformation( "Informações atualizadas com sucesso." );
            return NoContent();
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> Delete( Guid id )
        {
            var cliente = await _service.DeleteAsync( id );

            if (cliente == null)
            {
                return NotFound();
            }
            _logger.LogInformation( "Informações excluídas com sucesso." );
            return NoContent();
        }
    }
}
