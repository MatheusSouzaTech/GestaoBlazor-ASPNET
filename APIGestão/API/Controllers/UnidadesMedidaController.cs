using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/unidadesmedida")]
    public class UnidadesMedidaController : ControllerBase
    {
        private readonly UnidadeMedidaService _service;
        public UnidadesMedidaController(UnidadeMedidaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarUnidades());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var u = await _service.BuscarUnidadeId(id);
            return u == null ? NotFound() : Ok(u);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnidadeMedida u)
        {
            var (sucesso, mensagem) = await _service.Adicionar(u);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UnidadeMedida u)
        {
            var (sucesso, mensagem) = await _service.Atualizar(id, u);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.Excluir(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}
