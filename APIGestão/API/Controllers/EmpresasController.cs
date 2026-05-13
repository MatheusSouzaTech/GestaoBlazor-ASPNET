using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : ControllerBase
    {
        private readonly EmpresaService _service;
        public EmpresasController(EmpresaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarEmpresas());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empresa = await _service.BuscarEmpresaId(id);
            return empresa == null ? NotFound() : Ok(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Empresa empresa)
        {
            var (sucesso, mensagem) = await _service.AdicionarEmpresa(empresa);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Empresa empresa)
        {
            var (sucesso, mensagem) = await _service.AtualizarEmpresa(id, empresa);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.ExcluirEmpresa(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}
