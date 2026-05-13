using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _service;
        public CategoriasController(CategoriaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarCategorias());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _service.BuscarCategoriaId(id);
            return c == null ? NotFound() : Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Categoria c)
        {
            var (sucesso, mensagem) = await _service.AdicionarCategoria(c);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria c)
        {
            var (sucesso, mensagem) = await _service.AtualizarCategoria(id, c);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.ExcluirCategoria(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}
