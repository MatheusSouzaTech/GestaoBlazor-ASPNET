using APIGestão.API.Models;
using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportadoraController : ControllerBase
    {
        private readonly TransportadoraService _service;

        public TransportadoraController(TransportadoraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transportadoras = await _service.ListarTransportadoras();
            return Ok(transportadoras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transportadora = await _service.BuscarTransportadoraId(id);
            if (transportadora == null) return NotFound();
            return Ok(transportadora);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transportadora transportadora)
        {
            var resultado = await _service.AdicionarTransportadora(transportadora);
            if (!resultado) return BadRequest();
            return Created(string.Empty, transportadora);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Transportadora transportadora)
        {
            var resultado = await _service.AtualizarTransportadora(id, transportadora);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.ExcluirTransportadora(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}
