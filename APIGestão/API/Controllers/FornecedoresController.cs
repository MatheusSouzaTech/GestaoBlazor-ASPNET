using ModelsLibrary.Models;
using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/fornecedores")]
    public class FornecedoresController : ControllerBase
    {
        private readonly FornecedorService _service;
        public FornecedoresController(FornecedorService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarFornecedores());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fornecedor = await _service.BuscarFornecedorId(id);
            return fornecedor == null ? NotFound() : Ok(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Fornecedor fornecedor)
        {
            var (sucesso, mensagem) = await _service.AdicionarFornecedor(fornecedor);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Fornecedor fornecedor)
        {
            var (sucesso, mensagem) = await _service.AtualizarFornecedor(id, fornecedor);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.ExcluirFornecedor(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}

