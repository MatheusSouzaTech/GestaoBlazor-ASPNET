using APIGestão.API.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;

namespace APIGestão.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;
        public ClientesController(ClienteService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.ListarClientes());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _service.BuscarClienteId(id);
            return cliente == null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            var (sucesso, mensagem) = await _service.AdicionarCliente(cliente);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            var (sucesso, mensagem) = await _service.AtualizarCliente(id, cliente);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (sucesso, mensagem) = await _service.ExcluirCliente(id);
            return sucesso ? Ok(new { mensagem }) : BadRequest(new { mensagem });
        }
    }
}
