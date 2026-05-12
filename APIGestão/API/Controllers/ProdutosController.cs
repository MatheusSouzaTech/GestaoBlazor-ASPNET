using APIGestão.API.Models;
using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutosController(ProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _service.ListarProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _service.BuscarProdutoId(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Produtos produto)
        {
            var resultado = await _service.AdicionarProdutos(produto);
            if (!resultado) return BadRequest();
            return Created(string.Empty, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Produtos produto)
        {
            var resultado = await _service.UpdateProdutos(id, produto);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.ExcluirProdutos(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}
