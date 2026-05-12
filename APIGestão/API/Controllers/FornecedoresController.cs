using APIGestão.API.Models;
using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly FornecedorService _service;

        public FornecedoresController(FornecedorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fornecedores = await _service.ListarFornecedores();
            return Ok(fornecedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fornecedor = await _service.BuscarFornecedorId(id);
            if (fornecedor == null) return NotFound();
            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Fornecedores fornecedor)
        {
            var resultado = await _service.AdicionarFornecedor(fornecedor);
            if (!resultado) return BadRequest();
            return Created(string.Empty, fornecedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Fornecedores fornecedor)
        {
            var resultado = await _service.AtualizarFornecedor(id, fornecedor);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.ExcluirFornecedor(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}
