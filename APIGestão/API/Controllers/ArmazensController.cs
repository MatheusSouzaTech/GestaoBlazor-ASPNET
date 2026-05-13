using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/armazens")]
    public class ArmazensController : ControllerBase
    {
        private readonly ArmazemService _service;
        public ArmazensController(ArmazemService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarArmazens());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var a = await _service.BuscarArmazemId(id);
            return a == null ? NotFound() : Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Armazem a)
        {
            var (sucesso, mensagem) = await _service.Adicionar(a);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Armazem a)
        {
            var (sucesso, mensagem) = await _service.Atualizar(id, a);
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
