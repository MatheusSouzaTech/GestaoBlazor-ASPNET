using APIGestão.API.Models;
using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly MarcaService _service;

        public MarcasController(MarcaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var marcas = await _service.ListarMarcas();
            return Ok(marcas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var marca = await _service.BuscarMarcaId(id);
            if (marca == null) return NotFound();
            return Ok(marca);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Marcas marca)
        {
            var resultado = await _service.AdicionarMarca(marca);
            if (!resultado) return BadRequest();
            return Created(string.Empty, marca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Marcas marca)
        {
            var resultado = await _service.AtualizarMarca(id, marca);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.ExcluirMarca(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}
