using ModelsLibrary.Models;
using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/marcas")]
    public class MarcasController : ControllerBase
    {
        private readonly MarcaService _service;
        public MarcasController(MarcaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarMarcas());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var m = await _service.BuscarMarcaId(id);
            return m == null ? NotFound() : Ok(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Marcas m)
        {
            var (sucesso, mensagem) = await _service.AdicionarMarca(m);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Marcas m)
        {
            var (sucesso, mensagem) = await _service.AtualizarMarca(id, m);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.ExcluirMarca(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}
