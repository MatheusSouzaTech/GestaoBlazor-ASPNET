using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/filiais")]
    public class FiliaisController : ControllerBase
    {
        private readonly FilialService _service;
        public FiliaisController(FilialService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarFiliais());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var filial = await _service.BuscarFilialId(id);
            return filial == null ? NotFound() : Ok(filial);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Filial filial)
        {
            var (sucesso, mensagem) = await _service.AdicionarFilial(filial);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Filial filial)
        {
            var (sucesso, mensagem) = await _service.AtualizarFilial(id, filial);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.ExcluirFilial(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}
